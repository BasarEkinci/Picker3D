using Runtime.Commands.Player;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        public byte StageValue;
        internal ForceBallsToPoolCommand ForceCommand;

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerPhysicsController physicsController;

        private PlayerData _data;


        #endregion
        
        private void Awake()
        {
            _data = GetPlayerData();
            SendDataToControllers();
            Init();
        }
        private void SendDataToControllers()
        {
            movementController.SetData(_data.MovementData);
            meshController.SetData(_data.MeshData);
        }
        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }
        private void Init()
        {
            ForceCommand = new ForceBallsToPoolCommand(this,_data.ForceData);
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            InputSignals.Instance.OnInputTaken += () => movementController.IsReadyToPlay(true);
            InputSignals.Instance.OnInputReleased += () => movementController.IsReadyToMove(false);
            InputSignals.Instance.OnInputDragged += OnInputDragged;
            UISignals.Instance.OnPlay += () => movementController.IsReadyToPlay(true);
            CoreGameSignals.Instance.OnLevelSuccessful += () => movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.OnLevelFailed += () => movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.OnStageAreaEntered += () => movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.OnStageAreaSuccessful += OnStageAreaSuccessful;
            CoreGameSignals.Instance.OnFinishAreaEntered += OnFinishAreaEntered;
            CoreGameSignals.Instance.OnReset += OnReset;
        }
        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputParams(inputParams);
        }
        private void OnStageAreaSuccessful(byte value)
        {
            StageValue = ++value;
            movementController.IsReadyToPlay(true);
            meshController.PlayConfetti();
            meshController.ShowUpText();
        }
        private void OnFinishAreaEntered()
        {
            CoreGameSignals.Instance.OnLevelSuccessful?.Invoke();
            //Mini game should be written
        }
        private void OnReset()
        {
            StageValue = 0;
            movementController.OnReset();
            physicsController.OnReset();
            meshController.OnReset();
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.OnInputTaken -= () => movementController.IsReadyToPlay(true);
            InputSignals.Instance.OnInputReleased -= () => movementController.IsReadyToMove(false);
            InputSignals.Instance.OnInputDragged -= OnInputDragged;
            UISignals.Instance.OnPlay -= () => movementController.IsReadyToPlay(true);
            CoreGameSignals.Instance.OnLevelSuccessful -= () => movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.OnLevelFailed -= () => movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.OnStageAreaEntered -= () => movementController.IsReadyToPlay(false);;
            CoreGameSignals.Instance.OnStageAreaSuccessful -= OnStageAreaSuccessful;
            CoreGameSignals.Instance.OnFinishAreaEntered -= OnFinishAreaEntered;
            CoreGameSignals.Instance.OnReset -= OnReset;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}