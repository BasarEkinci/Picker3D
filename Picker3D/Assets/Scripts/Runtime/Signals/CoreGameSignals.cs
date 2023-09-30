using System;
using Runtime.Extentions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoSingelton<CoreGameSignals>
    {
        public UnityAction<byte> OnLevelInitialize = delegate { };
        public UnityAction OnClearActiveLevel = delegate { };
        public UnityAction OnLevelSuccessful = delegate { };
        public UnityAction OnLevelFailed = delegate { };
        public UnityAction OnNextLevel = delegate { };
        public UnityAction OnRestartLevel = delegate { };
        public UnityAction OnReset = delegate { };
        public Func<byte> OnGetLevelValue = delegate { return 0; };
        public UnityAction OnStageAreaEntered = delegate { };
        public UnityAction<byte> OnStageAreaSuccessful = delegate { };
        public UnityAction OnFinishAreaEntered = delegate { };
    }
}