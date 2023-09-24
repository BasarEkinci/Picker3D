using System;
using Cinemachine;
using Runtime.Signals;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        
        private float3 _firstPosition;

        private void Start()
        {
            Init();
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        private void Init()
        {
            _firstPosition = transform.position;
        }
        private void UnsubscribeEvents()
        {
            CameraSignals.Instance.OnSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.OnReset -= OnReset;
        }

        private void SubscribeEvents()
        {
            CameraSignals.Instance.OnSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.OnReset += OnReset;
        }

        private void OnReset()
        {
            transform.position = _firstPosition;
        }

        private void OnSetCameraTarget()
        {
            //var player = FindObjectOfType<PlayerManager>().transform;
            //virtualCamera.Follow = player;
            ////virtualCamera.LookAt = player;
        }
    }
}