using Runtime.Extentions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CameraSignals : MonoSingelton<CameraSignals>
    {
        public UnityAction OnSetCameraTarget = delegate {  }; 
    }
}