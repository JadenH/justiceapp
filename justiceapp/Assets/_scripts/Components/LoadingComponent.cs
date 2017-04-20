using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _scripts
{
    [GameState(GameState = GameState.Loading)]
    public class LoadingComponent : DragonSubscriber<DragonStore<string>>, IDragonAnimator
    {
        public Text LoadingText;

        private void Start()
        {
            SubscribeTo(Dragon.Model.Value.LoadingMessage);
        }

        public Sequence AnimateIn()
        {
            return DOTween.Sequence().Append(GetComponent<CanvasGroup>().DOFade(1f, .5f));
        }

        public Sequence AnimateOut()
        {
            return DOTween.Sequence().Append(GetComponent<CanvasGroup>().DOFade(0f, .5f));
        }

        protected override void OnSubscribe(DragonStore<string> dragonObject)
        {
            dragonObject.OnChange(this, (s, prev) =>
            {
                LoadingText.text = s;
            });
        }

        protected override void OnUnsubscribe()
        {
        }
    }
}
