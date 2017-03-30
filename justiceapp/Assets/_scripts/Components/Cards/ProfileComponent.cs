using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _scripts.Model;

namespace _scripts
{
    [GameState(GameState = GameState.Profile)]
    public class ProfileComponent : Card, IDragonAnimator
    {
        public Image ProfilePicture;
        public Text Offense;

        public Text Counter;
        public Text MinText;
        public Text MaxText;

        private void Start()
        {
#if UNITY_EDITOR
            // FOR TESTING ONLY
            //            StartCoroutine(Temp());
#endif
        }

        private IEnumerator Temp()
        {
            yield return new WaitForSeconds(0);
            if (Random.Range(0, 2) == 0)
            {
                SwipeLeft();
            }
            else
            {
                SwipeRight();
            }
        }

        public void SetProfile(Profile profile, int valueTotal)
        {
            transform.DOLocalMove(Vector2.up * Screen.height * 2, 0f);

            Offense.text = "";
            MinText.DOFade(0f, 0);
            MaxText.DOFade(0f, 0);

            ProfilePicture.sprite = ProfileData.Instance.GetProfilePicture(profile.FileName);

            Counter.text = valueTotal + "/50";
            MaxText.text = profile.Max == "Life" ? profile.Max : profile.Max + " months";
            MinText.text = profile.Min == "Life" ? profile.Min : profile.Min + " months";

            var sequence = DOTween.Sequence().SetDelay(.5f);
            sequence.Append(Offense.DOText(profile.Offense ?? "N/A", .8f));
            sequence.Append(MinText.DOFade(1f, .5f));
            sequence.Append(MaxText.DOFade(1f, .5f));
            sequence.Play();
        }

        protected override bool SwipeDown()
        {
            GameState.Profile.Set(State.Disabled);
            GameState.ProfileDetails.Set(State.Enabled);
            transform.DOLocalMove(Vector2.up * Screen.height * 2, 1f);
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
