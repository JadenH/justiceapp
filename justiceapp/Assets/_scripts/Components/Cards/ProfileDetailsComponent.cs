using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _scripts.Model;

namespace _scripts
{
    [GameState(GameState = GameState.ProfileDetails)]
    public class ProfileDetailsComponent : Card, IDragonAnimator
    {
        public Text CitizenShip;
        public Text DateOfBirth;
        public Text Education;
        public Text Employment;
        public Text Gender;
        public Text MaritalStatus;
        public Text Max;
        public Text Min;
        public Text Offense;
        public Text PriorDrugOffense;
        public Text PriorFelonies;
        public Text PriorSexOffenses;
        public Text PriorViolentFelonies;
        public Text Race;
        public Text Zipcode;

        public Text MinText;
        public Text MaxText;
        public Text Counter;

        public void SetProfile(Profile profile, int valueTotal)
        {
            transform.DOLocalMove(Vector2.down * Screen.height * 2, 0f);

            CitizenShip.text = profile.Citizenship;
            DateOfBirth.text = profile.DateOfBirth;
            Education.text = profile.Education;
            Employment.text = profile.Employment;
            Gender.text = profile.Gender;
            MaritalStatus.text = profile.MaritalStatus;
            Max.text = profile.Max == "Life" ? profile.Max : profile.Max + " months";
            Min.text = profile.Min == "Life" ? profile.Min : profile.Min + " months";
            Offense.text = profile.Offense;
            PriorDrugOffense.text = profile.PriorDrugOffense;
            PriorFelonies.text = profile.PriorFelonies;
            PriorSexOffenses.text = profile.PriorSexOffenses;
            PriorViolentFelonies.text = profile.PriorViolentFelonies;
            Race.text = profile.Race;
            Zipcode.text = profile.Zipcode;

            Counter.text = valueTotal + "/50";
            MaxText.text = profile.Max == "Life" ? profile.Max : profile.Max + " months";
            MinText.text = profile.Min == "Life" ? profile.Min : profile.Min + " months";

            AlternateColors();
        }

        private void AlternateColors()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i)
                    .GetComponent<Image>()
                    .color = i % 2 == 0 ? new Color32(94, 94, 94, 255) : new Color32(116, 116, 116, 255);
            }
        }

        protected override bool SwipeUp()
        {
            GameState.Profile.Set(State.Enabled);
            GameState.ProfileDetails.Set(State.Disabled);
            transform.DOLocalMove(Vector2.down * Screen.height * 2, 1f);
            return false;
        }

        protected override bool SwipeLeft()
        {
            GameState.Profile.Set(State.Disabled);
            GameState.ProfileDetails.Set(State.Disabled);
            transform.DOLocalMove(Vector2.left * Screen.width * 2, 1f);
            Dispatcher.DispatchDelay(Events.Min, "{}", .6f);
            return false;
        }

        protected override bool SwipeRight()
        {
            GameState.Profile.Set(State.Disabled);
            GameState.ProfileDetails.Set(State.Disabled);
            transform.DOLocalMove(Vector2.right * Screen.width * 2, 1f);
            Dispatcher.DispatchDelay(Events.Max, "{}", .6f);
            return false;
        }

        public Sequence AnimateIn()
        {
            return DOTween.Sequence().Append(GetComponent<CanvasGroup>().DOFade(1f, .5f)).Join(transform.DOMove(StartingPosition, .5f));
        }

        public Sequence AnimateOut()
        {
            return DOTween.Sequence().Append(GetComponent<CanvasGroup>().DOFade(0f, .5f));
        }
    }
}
