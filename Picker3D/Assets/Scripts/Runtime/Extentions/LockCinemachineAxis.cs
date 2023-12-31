﻿using System;
using Cinemachine;
using UnityEngine;

namespace Runtime.Extentions
{
    public enum CinemachineLockAxis
    {
        X,
        Y,
        Z
    }
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class LockCinemachineAxis : CinemachineExtension
    {
        [Tooltip("Lock the Cinemachine Virtual camera's X axis position to this value")]
        public float XClampValue = 0;

        [SerializeField]private CinemachineLockAxis lockAxis;
        
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            switch (lockAxis)
            {
                case CinemachineLockAxis.X:
                    if (stage == CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.x = XClampValue;
                        state.RawPosition = pos;
                    }
                    break;
                case CinemachineLockAxis.Y:
                    if (stage == CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.y = XClampValue;
                        state.RawPosition = pos;
                    }
                    break;
                case CinemachineLockAxis.Z:
                        if (stage == CinemachineCore.Stage.Body)
                        {
                            var pos = state.RawPosition;
                            pos.z = XClampValue;
                            state.RawPosition = pos;
                        }
                        break;
                default:
                    throw new ArgumentOutOfRangeException();
                    
            }

        }
    }
}