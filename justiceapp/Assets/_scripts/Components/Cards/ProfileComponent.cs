﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _scripts.Cards;
using _scripts.Model;

namespace _scripts
{
    [GameState(GameState = GameState.Profile)]
    public class ProfileComponent : ProfileCard, IDragonAnimator
    {
        public Image ProfilePicture;
        public Text Offense;

        public Text Counter;
        public Text MinText;
        public Text MaxText;

        public CanvasGroup RankGroup;
        public Text RankText;

        public Button ArrowButton;

        private void Start()
        {
#if UNITY_EDITOR
            // FOR TESTING ONLY
//            StartCoroutine(Temp());
#endif
            ArrowButton.onClick.AddListener(() =>
            {
                SwipeDown();
                SwipeCardAnimate(Direction.Down);
            });
            RankGroup.alpha = 0;
        }

        private IEnumerator Temp()
        {
            yield return new WaitForSeconds(1);
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
            RankText.text = profile.Prediction != null ? "Personal Sentencing Score: " + profile.Prediction : "";

            var sequence = DOTween.Sequence().SetDelay(.5f);
            sequence.Append(Offense.DOText(profile.Offense ?? "N/A", .8f));
            sequence.Append(MinText.DOFade(1f, .5f));
            sequence.Append(MaxText.DOFade(1f, .5f));

            RankGroup.gameObject.SetActive(profile.Prediction != null);
            AutoSentencing(profile, sequence);

            sequence.Play();
        }

        private void AutoSentencing(Profile profile, Sequence sequence)
        {
            if (profile.Prediction != null)
            {
                sequence.Prepend(RankGroup.DOFade(1f, .5f));

                if (Dragon.Model.Value.Total.Value >= 45)
                {
                    DisableCardSwipe = true;
                    sequence.AppendInterval(0.5f);
                    sequence.AppendCallback(() =>
                    {
                        if (profile.Prediction > 5)
                        {
                            SwipeRight();
                            transform.DOLocalMove(Vector2.right * Screen.width * 2, 1f);
                        }
                        else
                        {
                            SwipeLeft();
                            transform.DOLocalMove(Vector2.left * Screen.width * 2, 1f);
                        }
                    });
                }
            }
        }

        protected override bool SwipeDown()
        {
            GameState.Profile.Set(State.Disabled);
            GameState.ProfileDetails.Set(State.Enabled);
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
