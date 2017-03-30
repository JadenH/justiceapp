using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _scripts.Model;

namespace _scripts.Finish
{
    [GameState(GameState = GameState.Finish)]
    public class FinishComponent : DragonSubscriber<DragonStore<JusticeModel>>, IDragonAnimator
    {
        public Text SentencingText;
        public CanvasGroup MinPanelGroup;
        public CanvasGroup MaxPanelGroup;
        public CanvasGroup InfoButtonGroup;

        public Text MinText;
        public Text MaxText;
        public Button InfoButton;

        private void Start()
        {
            SubscribeTo(Dragon.Model);
            InfoButton.onClick.AddListener(() => Application.OpenURL("info.justiceexe.com"));
        }

        protected override void OnSubscribe(DragonStore<JusticeModel> dragonObject)
        {
            dragonObject.Value.MinTotal.OnChange(this, (i, prev) =>
            {
                MinText.text = i.ToString();
            });
            dragonObject.Value.MaxTotal.OnChange(this, (i, prev) =>
            {
                MaxText.text = i.ToString();
            });
        }

        protected override void OnUnsubscribe()
        {
        }

        public Sequence AnimateIn()
        {
            return DOTween.Sequence()
                .Append(GetComponent<CanvasGroup>().DOFade(1f, .5f))
                .Join(transform.DOScale(1f, .5f))
                .Append(SentencingText.DOFade(1f, .2f))
                .AppendInterval(.6f)
                .Append(MinPanelGroup.DOFade(1f, .2f))
                .AppendInterval(.6f)
                .Append(MaxPanelGroup.DOFade(1f, .2f))
                .AppendInterval(.6f)
                .Append(InfoButtonGroup.DOFade(1f, .2f));
        }

        public Sequence AnimateOut()
        {
            return DOTween.Sequence()
                .Append(GetComponent<CanvasGroup>().DOFade(0f, .5f))
                .Join(transform.DOScale(0f, .5f))
                .Append(SentencingText.DOFade(0f, .2f))
                .Append(MinPanelGroup.DOFade(0f, .2f))
                .Append(MaxPanelGroup.DOFade(0f, .2f))
                .Append(InfoButtonGroup.DOFade(0f, .2f));
        }
    }
}