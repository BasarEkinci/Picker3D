using Runtime.Enums;
using Runtime.Extentions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreUISignals : MonoSingelton<CoreUISignals>
    {
        public UnityAction<UIPanelTypes,int> OnOpenPanel = delegate { };
        public UnityAction<int> OnClosePanel = delegate { };
        public UnityAction OnCloseAllPanels = delegate { };        
    }
}