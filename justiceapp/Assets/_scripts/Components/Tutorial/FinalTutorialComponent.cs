using System;
using DG.Tweening;
using UnityEngine;

namespace _scripts.Tutorial
{
    [GameState(GameState = GameState.FinalTutorial)]
    public class FinalTutorialComponent : TutorialComponent
    {
        protected override bool SwipeRight()
        {
            GameState.FinalTutorial.Set(State.Disabled);
            GameState.CoreLoop.Set(State.Enabled, .6f);
            transform.DOLocalMove(Vector2.right*Screen.width, 1f);
            return false;
        }

        protected override bool SwipeLeft()
        {
            GameState.FinalTutorial.Set(State.Disabled);
            GameState.CoreLoop.Set(State.Enabled, .6f);
            transform.DOLocalMove(Vector2.left * Screen.width, 1f);
            return false;
        }
    }
}