using System.Collections.Generic;
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
        private DragonStore<int> Round { get; set; }

        public DragonStore<int> MinTotal { get; private set; }
        public DragonStore<int> MaxTotal { get; private set; }
        public DragonStore<int> Total { get; private set; }

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

            // Get Initial Server Data
            Server.Instance.FetchProfiles("start", Round.Value);
        }

        [Event(Event = Events.NewProfiles)]
        public void NewProfiles(JObject json)
        {
            GameId.Value = json["id"].ToString();
            Profiles.Value = json["profiles"].ToObject<Queue<Profile>>();
            AllProfiles.Value.AddRange(json["profiles"].ToObject<List<Profile>>());

            CurrentProfile.Value = Profiles.Value.Dequeue();
        }

        [Event(Event = Events.Min)]
        public void MinSentence(JObject json)
        {
            Total.Value++;
            MinTotal.Value++;
            Swipes.Add(0);
            DoFetch();
        }

        [Event(Event = Events.Max)]
        public void MaxSentence(JObject json)
        {
            Total.Value++;
            MaxTotal.Value++;
            Swipes.Add(1);
            DoFetch();
        }

        private void DoFetch()
        {
            if (Swipes.Count == 10 && Total.Value < 50)
            {
                Server.Instance.FetchProfiles(GameId.Value, Round.Value, Swipes.ToArray());
                Swipes.Clear();
                Round.Value++;
            }
            else if (Total.Value == 50)
            {
                GameState.CoreLoop.Set(State.Disabled);
                GameState.Finish.Set(State.Enabled);
            }
            else
            {
                CurrentProfile.Value = Profiles.Value.Dequeue();
            }
        }
    }
}
