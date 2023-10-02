using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Signals;
using TMPro;
using UnityEngine.Serialization;

namespace Runtime.Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private TextMeshPro poolText;
        [SerializeField] private byte stageID;
        [SerializeField] private Renderer renderer;
        [SerializeField] private Color poolComletedColor;
        
        #endregion
        
        #region Private Variables

        private PoolData _data;
        private byte _collectedCount;
        private readonly string _collectableTag = "Collectable";

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPoolData();
            
        }

        private void Start()
        {
            SetRequiredAmountText();
        }

        private void SetRequiredAmountText()
        {
            poolText.text = $"0/{_data.RequiredObjectCount}";
        }

        private PoolData GetPoolData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[(int) CoreGameSignals.Instance.OnGetLevelValue?.Invoke()].PoolDatas[stageID];
        }

        private void OnEnable()
        {
            SubcribeEvents();
        }

        private void SubcribeEvents()
        {
            CoreGameSignals.Instance.OnStageAreaSuccessful += OnActivateTweens;
            CoreGameSignals.Instance.OnLevelInitialize += OnChangePoolColor;
        }

        private void OnChangePoolColor(byte arg0)
        {
            
        }
        public bool TakeResults(byte managerStateValue)
        {
            if (stageID == managerStateValue)
            {
                return _collectedCount >= _data.RequiredObjectCount;
            }
            return false;
        }
        private void OnActivateTweens(byte stageValue)
        {
            if(stageValue != stageID) return;
            renderer.material.DOColor(new Color(0.2078432f,0.3058824f,0.5294118f), 0.5f).SetEase(Ease.Linear).SetRelative(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_collectableTag)) return;
            IncreaseCollectedAmount();
            SetCollectedAmoundToPool();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(_collectableTag)) return;
            DecreaseCollectedAmount();
            SetCollectedAmoundToPool();
        }

        private void DecreaseCollectedAmount()
        {
            _collectedCount--;
        }

        private void SetCollectedAmoundToPool()
        {
            poolText.text = $"{_collectedCount}/{_data.RequiredObjectCount}";   
        }
        private void IncreaseCollectedAmount()
        {
            _collectedCount++;
        }
    }
}