using System;
using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Handlers
{
    public class UIEventSubsciber : MonoBehaviour
    {
        [SerializeField] private UIEventSubscriptionTypes type;
        [SerializeField] private Button button;

        private UIManager _manager;

        private void Awake()
        {
            GetReferences();
        }
        private void GetReferences()
        {
            _manager = FindObjectOfType<UIManager>();
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                    button.onClick.RemoveListener(_manager.Play);
                    break;
                case UIEventSubscriptionTypes.OnNextLevel:
                    button.onClick.RemoveListener(_manager.NextLevel);
                    break;
                case UIEventSubscriptionTypes.OnRestartLevel:
                    button.onClick.RemoveListener(_manager.RestartLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                    button.onClick.AddListener(_manager.Play);
                    break;
                case UIEventSubscriptionTypes.OnNextLevel:
                    button.onClick.AddListener(_manager.NextLevel);
                    break;
                case UIEventSubscriptionTypes.OnRestartLevel:
                    button.onClick.AddListener(_manager.RestartLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}