using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace _scripts.Model
{
    public class JusticeModel
    {
        public DragonStore<string> GameId { get; private set; }
        public DragonStore<Profile> CurrentProfile { get; private set; }

        public DragonStore<Queue<Profile>> Profiles { get; private set; }
        public DragonStore<List<Profile>> AllProfiles { get; private set; }
        private List<int> Swipes { get; set; }
        public DragonStore<int> Round { get; set; }

        public DragonStore<int> MinTotal { get; private set; }
        public DragonStore<int> MaxTotal { get; private set; }
        public DragonStore<int> Total { get; private set; }

        public DragonStore<FinalData> FinalData { get; private set; }

        public JusticeModel()
        {
            GameId = new DragonStore<string>("start");
            CurrentProfile = new DragonStore<Profile>(new Profile());
            Round = new DragonStore<int>(1);

            Total = new DragonStore<int>(0);
            MinTotal = new DragonStore<int>(0);
            MaxTotal = new DragonStore<int>(0);

            Swipes = new List<int>();
            Profiles = new DragonStore<Queue<Profile>>(new Queue<Profile>());
            AllProfiles = new DragonStore<List<Profile>>(new List<Profile>());

            FinalData = new DragonStore<FinalData>(new FinalData());

            // Get Initial Server Data
            FetchProfiles(new JObject());
        }

        [Event(Event = Events.NewProfiles)]
        public void NewProfiles(JObject json)
        {
            GameId.Value = json["id"].ToString();
            foreach (var profile in json["profiles"].ToObject<List<Profile>>())
            {
                Profiles.Value.Enqueue(profile);
            }
            AllProfiles.Value.AddRange(json["profiles"].ToObject<List<Profile>>());

            CurrentProfile.Value = Profiles.Value.Dequeue();
        }

        [Event(Event = Events.Min)]
        public void MinSentence(JObject json)
        {
            Total.Value++;
            MinTotal.Value++;
            Swipes.Add(0);
            CheckRound();
        }

        [Event(Event = Events.Max)]
        public void MaxSentence(JObject json)
        {
            Total.Value++;
            MaxTotal.Value++;
            Swipes.Add(1);
            CheckRound();
        }

        [Event(Event = Events.Final)]
        public void ReceiveFinal(JObject json)
        {
            FinalData.Value = json.ToObject<FinalData>();
            GameState.FinishLoading.Set(State.Disabled);
            GameState.Finish.Set(State.Enabled);
        }

        [Event(Event = Events.FetchProfiles)]
        public void FetchProfiles(JObject json)
        {
            Server.Instance.FetchProfiles(GameId.Value, Round.Value, Swipes.ToArray());
            Swipes.Clear();
            Round.Value++;
        }

        private void CheckRound()
        {
            // If we are out of profiles and we haven't swiped 50 profiles.
            if (!Profiles.Value.Any() && Round.Value <= 5)
            {
                Debug.Assert(Profiles.Value.Count == 0);
                GameState.Loading.Set(State.Enabled);
                Dragon.Instance.Dispachter.DispatchDelay(Events.FetchProfiles, "{}", 5f);
            }
            // If we have swiped 50 profiles.
            else if (Total.Value == 50)
            {
                Server.Instance.Finish(GameId.Value);

                GameState.CoreLoop.Set(State.Disabled);
                GameState.FinishLoading.Set(State.Enabled);

            }
            // We still have profiles to iterate through..
            else
            {
                CurrentProfile.Value = Profiles.Value.Dequeue();
            }
        }
    }
}
