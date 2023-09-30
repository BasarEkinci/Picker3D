using Runtime.Extentions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class UISignals : MonoSingelton<UISignals>
    {
        public UnityAction<byte> OnSetStageColor = delegate {  };
        public UnityAction<byte> OnSetLevelValue = delegate {  };
        public UnityAction OnPlay = delegate {  };
    }
}