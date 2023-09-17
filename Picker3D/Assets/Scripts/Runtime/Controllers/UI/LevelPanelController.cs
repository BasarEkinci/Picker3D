using System.Collections.Generic;
using DG.Tweening;
using Runtime.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        [SerializeField] private List<Image> stageImages;
        [SerializeField] private List<TextMeshProUGUI> levelTexts;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.OnSetLevelValue += OnSetLevelValue;
            UISignals.Instance.OnSetStageColor += OnSetStageColor;
        }
        private void OnSetStageColor(byte stageValue)
        {
            stageImages[stageValue].DOColor(new Color(1f,0.4070103f,0,1f), 0.5f);
        }
        private void OnSetLevelValue(byte levelValue)
        {
            var additionalValue = ++levelValue;
            levelTexts[0].text = additionalValue.ToString();
            additionalValue++;
            levelTexts[1].text = additionalValue.ToString();
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        private void UnsubscribeEvents()
        {
            UISignals.Instance.OnSetLevelValue -= OnSetLevelValue;
            UISignals.Instance.OnSetStageColor -= OnSetStageColor;
        }
    }
}