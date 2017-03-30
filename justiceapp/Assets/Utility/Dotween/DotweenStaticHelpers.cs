using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using _scripts;

namespace Utility.Statics
{
    public static class DotweenStaticHelpers
    {
        public static void SetGameState(this Sequence sequence, GameState gameState, State state)
        {
            
            Sequence s = DOTween.Sequence();
            s.AppendCallback(() =>
            {
                gameState.Set(state);
            });
            sequence.Append(s);
        }

        public static void WaitForGameState(this Sequence sequence, GameState gameState, State state)
        {
            sequence.WaitForCondition(() => gameState.Is(state));
        }

        public static void WaitForCondition(this Sequence sequence, Func<bool> condition)
        {
            sequence.AppendCallback(() =>
            {
                if (sequence.IsActive())
                {
                    sequence.Pause();
                    DotweenHelper.Instance.StartCoroutine(DotweenHelper.Instance.WaitForCondition(sequence, condition));
                }
            });
            sequence.AppendInterval(.1f);
        }


    }
}