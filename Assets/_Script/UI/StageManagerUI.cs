using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class StageManagerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text stageText;

        private Sequence stageSequence;

        public void ShowStage(string stageName)
        {
            stageSequence?.Kill();

            stageText.text = stageName;

            stageText.alpha = 0f;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(stageText.DOFade(1f, 0.5f));

            sequence.AppendInterval(1.5f);

            sequence.Append(stageText.DOFade(0f, 0.5f));
        }

        private void OnDestroy()
        {
            stageSequence?.Kill();
        }
    }
}
