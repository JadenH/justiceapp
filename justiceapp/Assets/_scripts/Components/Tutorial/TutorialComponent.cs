using System;
using DG.Tweening;
using TouchScript.Gestures;
using UnityEngine;

namespace _scripts.Tutorial
{
    public abstract class TutorialComponent : Card, IDragonAnimator
    {
        public PressGesture PressGesture;

        public void Start()
        {
            PressGesture.Pressed += HandlePress;
        }

        protected virtual void HandlePress(object sender, EventArgs eventArgs) {}

        public Sequence AnimateIn()
        {
            return DOTween.Sequence().Append(GetComponent<CanvasGroup>().DOFade(1f, .5f));
        }

        public Sequence AnimateOut()
        {
            return DOTween.Sequence().Append(GetComponent<CanvasGroup>().DOFade(0f, .5f));
        }
    }
}