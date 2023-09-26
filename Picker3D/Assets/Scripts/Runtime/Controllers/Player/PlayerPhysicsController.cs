using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController
    {
        #region Self Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;

        private const string StageAreaTag = "StageArea";
        private const string FinishAreaTag = "FinishArea";
        private const string MiniGameAreaTag = "MiniGameArea";

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(StageAreaTag))
            {
                manager.ForceCommand.Execute();
                CoreGameSignals.Instance.OnStageAreaEntered?.Invoke();
                InputSignals.Instance.OnDisableInput?.Invoke();
                
                //stage area control process
            }

            if (other.CompareTag(FinishAreaTag))
            {
                CoreGameSignals.Instance.OnFinishAreaEntered?.Invoke();
                InputSignals.Instance.OnDisableInput?.Invoke();
                CoreGameSignals.Instance.OnLevelSuccessful?.Invoke();
                return;
            }

            if (other.CompareTag(MiniGameAreaTag))
            {
                //write the mini game mechanics
            }
        }
        internal void OnReset(){}
    }
}