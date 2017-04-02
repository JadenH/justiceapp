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
            return false;
        }

        protected override bool SwipeLeft()
        {
            GameState.MainMenu.Set(State.Disabled);
            GameState.NarrativeTutorial.Set(State.Enabled, .6f);
            return false;
        }
    }
}