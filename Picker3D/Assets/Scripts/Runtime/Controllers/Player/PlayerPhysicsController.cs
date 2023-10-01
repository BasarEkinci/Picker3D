using System;
using DG.Tweening;
using Runtime.Controllers.Pool;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
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

                DOVirtual.DelayedCall(3f, () =>
                {
                    var result = other.transform.parent.GetComponentInChildren<PoolController>().TakeResults(manager.StageValue);
                    if (result)
                    {
                        CoreGameSignals.Instance.OnStageAreaSuccessful?.Invoke(manager.StageValue);
                        InputSignals.Instance.OnEnableInput?.Invoke();
                    }
                    else
                        CoreGameSignals.Instance.OnLevelFailed?.Invoke();
                });   
                return;
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var transform1 = manager.transform;
            var position1 = transform1.position;
            Gizmos.DrawSphere(new Vector3(position1.x,position1.y + 1f,position1.z + 1f), 1.35f);
        }

        internal void OnReset(){}
    }
}