using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _scripts.Cards
{
    public abstract class ProfileCard : Card
    {
        public Image RedImage;
        public Image BlueImage;
        protected float ImageFadeAmount = .5f;

        protected override void Awake()
        {
            base.Awake();

            if (RedImage) RedImage.DOFade(0f, 0);
            if (BlueImage) BlueImage.DOFade(0f, 0);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ScreenTransformGesture.Transformed += HandleTransform;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ScreenTransformGesture.Transformed -= HandleTransform;
        }

        private void HandleTransform(object sender, EventArgs e)
        {
            CheckColorFade();
        }

        protected override void ReturnCard()
        {
            base.ReturnCard();
            if (RedImage) RedImage.DOFade(0f, .5f);
            if (BlueImage) BlueImage.DOFade(0f, .5f);
        }

        protected override bool SwipeLeft()
        {
            GameState.Profile.Set(State.Disabled);
            GameState.ProfileDetails.Set(State.Disabled);
            Dispatcher.DispatchDelay(Events.Min, "{}", .6f);
            BlueImage.DOFade(ImageFadeAmount, .1f);
            return false;
        }

        protected override bool SwipeRight()
        {
            GameState.Profile.Set(State.Disabled);
            GameState.ProfileDetails.Set(State.Disabled);
            Dispatcher.DispatchDelay(Events.Max, "{}", .6f);
            RedImage.DOFade(ImageFadeAmount, .1f);
            return false;
        }

        private void CheckColorFade()
        {
            if (RedImage && BlueImage)
            {
                var distance = StartingPosition - transform.position;
                if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
                {
                    if (distance.x < -SwipeDistanceHorizontal)
                    {
                        RedImage.DOFade(ImageFadeAmount, .5f);
                        BlueImage.DOFade(0f, .5f);
                    }
                    else if (distance.x > SwipeDistanceHorizontal)
                    {
                        RedImage.DOFade(0f, .5f);
                        BlueImage.DOFade(ImageFadeAmount, .5f);
                    }
                    else
                    {
                        RedImage.DOFade(0f, .5f);
                        BlueImage.DOFade(0f, .5f);
                    }
                }
            }
        }
    }
}
