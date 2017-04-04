using UnityEngine;
using Utility;
using _scripts.Model;

namespace _scripts
{
    [GameState(GameState = GameState.CoreLoop)]
    public class ProfileBuilder : DragonSubscriber<DragonStore<JusticeModel>>
    {
        public ProfileComponent ProfileComponent;
        public ProfileDetailsComponent ProfileDetailsComponent;

        private void Start()
        {
            SubscribeTo(Dragon.Model);
        }

        protected override void OnSubscribe(DragonStore<JusticeModel> dragonObject)
        {
            dragonObject.Value.CurrentProfile.OnChange(this, (profile, prev) =>
            {
                if (profile.FileName == null) return;
                transform.RemoveAllChildren();
                Instantiate(ProfileDetailsComponent, transform, false).SetProfile(profile, Data.Value.Total.Value);
                Instantiate(ProfileComponent, transform, false).SetProfile(profile, Data.Value.Total.Value);
                GameState.Profile.Set(State.Enabled);
                GameState.Loading.Set(State.Disabled);
            });
        }

        protected override void OnUnsubscribe()
        {
            transform.RemoveAllChildren();
        }
    }
}