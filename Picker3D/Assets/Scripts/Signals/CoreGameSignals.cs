using System;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoBehaviour
    {
        public static CoreGameSignals Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
                Instance = this;
        }
        
        public UnityAction<byte> OnLevelInitialize = delegate {  };
        public UnityAction OnClearActiveLevel = delegate {  };
        public UnityAction OnNextLevel = delegate {  };
        public UnityAction OnRestartLevel = delegate {  };
        public UnityAction onReset = delegate {  };
        public Func<byte> OnGetLevelValue = delegate { return 0; };
    }
}