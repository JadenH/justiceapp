using System;
using DG.Tweening;
using UnityEngine;

namespace _scripts.Tutorial
{
    [GameState(GameState = GameState.MainMenu)]
    public class MainMenuComponent : TutorialComponent
    {
        protected override bool SwipeRight()
        {
            GameState.MainMenu.Set(State.Disabled);
            GameState.NarrativeTutorial.Set(State.Enabled, .6f);
            transform.DOLocalMove(Vector2.right * Screen.width, 1f);
            return false;
        }

        protected override bool SwipeLeft()
        {
            GameState.MainMenu.Set(State.Disabled);
            GameState.NarrativeTutorial.Set(State.Enabled, .6f);
            transform.DOLocalMove(Vector2.left * Screen.width, 1f);
            return false;
        }
    }
}