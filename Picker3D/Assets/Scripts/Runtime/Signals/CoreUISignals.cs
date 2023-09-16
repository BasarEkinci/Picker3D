using Runtime.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreUISignals : MonoBehaviour
    {
        #region Singelton
        
        public static CoreUISignals Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        #endregion
        
        public UnityAction<UIPanelTypes,int> OnOpenPanel = delegate { };
        public UnityAction<int> OnClosePanel = delegate { };
        public UnityAction OnCloseAllPanels = delegate { };        
    }
}