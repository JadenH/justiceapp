using System;
using DG.Tweening;
using UnityEngine;

namespace _scripts.Tutorial
{
    [GameState(GameState = GameState.SwipeUpTutorial)]
    public class SwipeUpTutorialComponent : TutorialComponent
    {
        protected override bool SwipeRight()
        {
            GameState.SwipeUpTutorial.Set(State.Disabled);
            GameState.FinalTutorial.Set(State.Enabled, .6f);
            transform.DOLocalMove(Vector2.right * Screen.width, 1f);
            return false;
        }

        protected override bool SwipeLeft()
        {
            GameState.SwipeUpTutorial.Set(State.Disabled);
            GameState.FinalTutorial.Set(State.Enabled, .6f);
            transform.DOLocalMove(Vector2.left * Screen.width, 1f);
            return false;
        }
    }
}