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

        public CanvasGroup InfoButtonGroup;
        public Button InfoButton;

        private void Start()
        {
            SubscribeTo(Dragon.Model);
            InfoButton.onClick.AddListener(() => Application.OpenURL("http://info.justiceexe.com"));
        }

        protected override void OnSubscribe(DragonStore<JusticeModel> dragonObject)
        {
            dragonObject.Value.Total.OnChange(this, (i, prev) =>
            {
                var propertyCounts = new Dictionary<string, int>();
                foreach (PropertyInfo property in typeof(Profile).GetProperties())
                {
                    var prop = property;
                    if (new[] { "Max", "Min", "FileName", "Prediction" }.Contains(prop.Name)) continue;
                    propertyCounts[prop.Name] = dragonObject.Value.AllProfiles.Value.Count(profile => prop.GetValue(profile, null) != null);
                }

                var ordered = propertyCounts.OrderByDescending(pair => pair.Value).ToList();


                Top1Text.text = Regex.Replace(ordered[0].Key, "(\\B[A-Z])", " $1");
                Top2Text.text = Regex.Replace(ordered[1].Key, "(\\B[A-Z])", " $1");
                Top3Text.text = Regex.Replace(ordered[2].Key, "(\\B[A-Z])", " $1");
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
                .Append(BodyText.DOFade(0f, .2f))
                .Append(InfoButtonGroup.DOFade(0f, .2f));
        }
    }
}