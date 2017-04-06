using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _scripts.Model;

namespace _scripts.Finish
{
    [GameState(GameState = GameState.Finish)]
    public class FinishComponent : DragonSubscriber<DragonStore<JusticeModel>>, IDragonAnimator
    {
        public Text TitleText;
        public Text SubtitleText;
        public Text BodyText;

        public Text Top1Text;
        public Text Top2Text;
        public Text Top3Text;

        public CanvasGroup Top1Group;
        public CanvasGroup Top2Group;
        public CanvasGroup Top3Group;

        public Text StatsText;

        public CanvasGroup InfoButtonGroup;
        public Button InfoButton;

        private void Start()
        {
            SubscribeTo(Dragon.Model);
            InfoButton.onClick.AddListener(() => Application.OpenURL("http://info.justiceexe.com"));
        }

        protected override void OnSubscribe(DragonStore<JusticeModel> dragonObject)
        {
            dragonObject.Value.FinalData.OnChange(this, (data, prev) =>
            {
                Top1Text.text = data.Properties[0];
                Top2Text.text = data.Properties[1];
                Top3Text.text = data.Properties[2];

                StatsText.text = data.FinalText;
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
                .Append(TitleText.DOFade(1f, .2f))
                .AppendInterval(.6f)
                .Append(SubtitleText.DOFade(1f, .2f))
                .AppendInterval(3f)
                .Append(Top1Group.DOFade(1f, .2f))
                .AppendInterval(1f)
                .Append(Top2Group.DOFade(1f, .2f))
                .AppendInterval(1f)
                .Append(Top3Group.DOFade(1f, .2f))
                .AppendInterval(1f)
                .Append(StatsText.DOFade(1f, .2f))
                .AppendInterval(2f)
                .Append(BodyText.DOFade(1f, .2f))
                .AppendInterval(2f)
                .Append(InfoButtonGroup.DOFade(1f, .2f));
        }

        public Sequence AnimateOut()
        {
            return DOTween.Sequence()
                .Append(GetComponent<CanvasGroup>().DOFade(0f, .5f))
                .Join(transform.DOScale(0f, .5f))
                .Append(TitleText.DOFade(0f, .2f))
                .Append(SubtitleText.DOFade(0f, .2f))
                .Append(Top1Group.DOFade(0f, .2f))
                .Append(Top2Group.DOFade(0f, .2f))
                .Append(Top3Group.DOFade(0f, .2f))
                .Append(StatsText.DOFade(0f, .2f))
                .Append(BodyText.DOFade(0f, .2f))
                .Append(InfoButtonGroup.DOFade(0f, .2f));
        }
    }
}