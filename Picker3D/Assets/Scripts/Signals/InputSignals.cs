using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals : MonoBehaviour
    {
        #region Singelton

        public static InputSignals Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
        }

        #endregion
        
        public UnityAction OnFistTimeTouchTaken = delegate { };
        public UnityAction OnEnableInput = delegate { };
        public UnityAction OnDisableInput = delegate { };
        public UnityAction OnInputTaken = delegate { };
        public UnityAction OnInputReleased = delegate { };
        public UnityAction<HorizontalInputParams> OnInputDragged = delegate { };
    }
}