using Runtime.Extentions;
using Runtime.Keys;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class InputSignals : MonoSingelton<InputSignals>
    {
        public UnityAction OnFistTimeTouchTaken = delegate { };
        public UnityAction OnEnableInput = delegate { };
        public UnityAction OnDisableInput = delegate { };
        public UnityAction OnInputTaken = delegate { };
        public UnityAction OnInputReleased = delegate { };
        public UnityAction<HorizontalInputParams> OnInputDragged = delegate { };
    }
}