﻿using System;
using DG.Tweening;
using UnityEngine;

namespace _scripts.Tutorial
{
    [GameState(GameState = GameState.NarrativeTutorial)]
    public class NarrativeTutorialComponent : TutorialComponent
    {
        protected override bool SwipeRight()
        {
            GameState.NarrativeTutorial.Set(State.Disabled);
            GameState.SwipingTutorial.Set(State.Enabled, .6f);
            transform.DOLocalMove(Vector2.right * Screen.width, 1f);
            return false;
        }

        protected override bool SwipeLeft()
        {
            GameState.NarrativeTutorial.Set(State.Disabled);
            GameState.SwipingTutorial.Set(State.Enabled, .6f);
            transform.DOLocalMove(Vector2.left * Screen.width, 1f);
            return false;
        }
    }
}