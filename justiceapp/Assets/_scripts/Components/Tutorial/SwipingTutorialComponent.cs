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
            GameState.CoreLoop.Set(State.Enabled, .6f);
            return false;
        }

        protected override bool SwipeLeft()
        {
            GameState.SwipingTutorial.Set(State.Disabled);
            GameState.CoreLoop.Set(State.Enabled, .6f);
            return false;
        }
    }
}