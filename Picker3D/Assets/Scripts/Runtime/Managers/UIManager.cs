using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.OnLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.OnLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.OnReset += OnReset;
            CoreGameSignals.Instance.OnStageAreaSuccessful+= OnStageAreaSuccessful;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.OnLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.OnLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.OnLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.OnReset -= OnReset;
            CoreGameSignals.Instance.OnStageAreaSuccessful-= OnStageAreaSuccessful;
        }
        private void OnStageAreaSuccessful(byte stageValue)
        {
            UISignals.Instance.OnSetStageColor?.Invoke(stageValue);
        }
        private void OnLevelInitialize(byte arg0)
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Level,0);
            UISignals.Instance.OnSetLevelValue?.Invoke((byte)CoreGameSignals.Instance.OnGetLevelValue?.Invoke());
        }
        private void OnReset()
        {
            CoreUISignals.Instance.OnCloseAllPanels?.Invoke();
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Start,1);
        }
        private void OnLevelFailed()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Fail,2);
        }
        private void OnLevelSuccessful()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Win,2);   
        }
        public void NextLevel()
        {
            CoreGameSignals.Instance.OnNextLevel?.Invoke();
        }
        public void RestartLevel()
        {
            CoreGameSignals.Instance.OnRestartLevel?.Invoke();
        }
        public void Play()
        {
            UISignals.Instance.OnPlay?.Invoke();
            CoreUISignals.Instance.OnClosePanel?.Invoke(1);
            InputSignals.Instance.OnEnableInput?.Invoke();
            CameraSignals.Instance.OnSetCameraTarget?.Invoke();
        }
    }
}