using DG.Tweening;
using UnityEngine;

namespace _scripts
{
    [GameState(GameState = GameState.FinishLoading)]
    public class FinishLoadingComponent : DragonBehaviour, IDragonAnimator
    {
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
