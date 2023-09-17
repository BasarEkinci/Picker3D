using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class UISignals : MonoBehaviour
    {
        #region Singelton
        
        public static UISignals Instance;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        #endregion

        public UnityAction<byte> OnSetStageColor = delegate {  };
        public UnityAction<byte> OnSetLevelValue = delegate {  };
        public UnityAction OnPlay = delegate {  };
        
    }
}