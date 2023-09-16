using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField] private List<Transform> layers;

        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();
        private void SubscribeEvents()
        {
            CoreUISignals.Instance.OnClosePanel += OnClosePanel;
            CoreUISignals.Instance.OnOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.OnCloseAllPanels += OnCloseAllPanels;
        }
        private void UnsubscribeEvents()
        {
            CoreUISignals.Instance.OnClosePanel -= OnClosePanel;
            CoreUISignals.Instance.OnOpenPanel -= OnOpenPanel;
            CoreUISignals.Instance.OnCloseAllPanels -= OnCloseAllPanels;
        }
        private void OnOpenPanel(UIPanelTypes panelTypes, int value)
        {
            OnClosePanel(value);
            Instantiate(Resources.Load<GameObject>($"Screens/ {panelTypes}Panel"), layers[value]);
        }
        private void OnClosePanel(int value)
        {
            if(layers[value].childCount <= 0) return;
            
#if UNITY_EDITOR
                DestroyImmediate(layers[value].GetChild(0).gameObject);
#else
                Destroy(layers[value].GetChild(0).gameObject);
#endif
            
        }
        private void OnCloseAllPanels()
        {
            foreach (var layer in layers)
            {
                if(layer.childCount <= 0) return;   
#if UNITY_EDITOR
                DestroyImmediate(layer.GetChild(0).gameObject);
#else
                Destroy(layer.GetChild(0).gameObject);
#endif
            }
        }
    }
}