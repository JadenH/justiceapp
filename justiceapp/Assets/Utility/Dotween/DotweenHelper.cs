using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Utility.Singleton;

namespace Utility
{
    public class DotweenHelper : MonoSingleton<DotweenHelper>
    {
        public IEnumerator WaitForCondition(Tween tween, Func<bool> condition)
        {
            yield return new WaitForEndOfFrame();
            while (tween.IsActive())
            {
                if (condition.Invoke())
                {
                    tween.Play();
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}