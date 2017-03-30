using System;
using DG.Tweening;
using UnityEngine;

namespace _scripts.Tutorial
{
    [GameState(GameState = GameState.SwipingTutorial)]
    public class SwipingTutorialComponent : TutorialComponent
    {
        protected override bool SwipeRight()
        {
            GameState.SwipingTutorial.Set(State.Disabled);
            GameState.SwipeUpTutorial.Set(State.Enabled, .6f);
            transform.DOLocalMove(Vector2.right * Screen.width, 1f);
            return false;
        }

        protected override bool SwipeLeft()
        {
            GameState.SwipingTutorial.Set(State.Disabled);
            GameState.SwipeUpTutorial.Set(State.Enabled, .6f);
            transform.DOLocalMove(Vector2.left * Screen.width, 1f);
            return false;
        }
    }
}