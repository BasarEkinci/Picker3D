using System.Collections.Generic;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Runtime.Signals;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        private InputData _data;
        private bool _isAvaibleForTouch;
        private bool _isFirtsTimeTouchTaken;
        private bool _isTouching;

        private float _currentVelocity;
        private float3 _moveVector;
        private Vector2? _mousePosition;


        private void Awake() => GetInputData();
        private void OnEnable()=> SunbscribeEvents();
        private void Update()
        {
            if(!_isAvaibleForTouch) return;
            
            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _isTouching = false;
                InputSignals.Instance.OnInputReleased?.Invoke();
            }
            
            if(Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                _isTouching = true;
                InputSignals.Instance.OnInputTaken?.Invoke();
                if (!_isFirtsTimeTouchTaken)
                {
                    _isFirtsTimeTouchTaken = true;
                    InputSignals.Instance.OnFistTimeTouchTaken?.Invoke();
                }
                
                _mousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                if (_isTouching)
                {
                    if (_mousePosition != null)
                    {
                        Vector2 mouseDeltaPos = (Vector2) Input.mousePosition -  _mousePosition.Value;
                        /*if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                        {
                            _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        }
                        else if(mouseDeltaPos.x < _data.HorizontalInputSpeed)
                        {
                            _moveVector.x = -_data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        }
                        else
                        {
                            _moveVector.x = Mathf.SmoothDamp(-_moveVector.x,0f,ref _currentVelocity,
                                _data.ClampSpeed);
                        }
                        _mousePosition = Input.mousePosition;
                        InputSignals.Instance.OnInputDragged?.Invoke(new HorizontalInputParams
                        {
                            HorizontalValue = _moveVector.x,
                            ClampValues = _data.ClampValues
                        });*/
                        _moveVector.x = mouseDeltaPos.x;
                    }
                }
            }
        }
        private void OnDisable() => UnsubscribeEvents();
        
        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData,results);
            return results.Count > 0;
        }

        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").Data;
        }


        private void OnDisableInput() => _isAvaibleForTouch = false;
        private void OnEnableInput() => _isAvaibleForTouch = true;
        private void SunbscribeEvents()
        {
            CoreGameSignals.Instance.OnReset += OnReset;
            InputSignals.Instance.OnEnableInput += OnEnableInput;
            InputSignals.Instance.OnDisableInput += OnDisableInput;
        }
        private void OnReset()
        {
            _isAvaibleForTouch = false;
            _isFirtsTimeTouchTaken = false;
            _isTouching = false;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.OnReset -= OnReset;
            InputSignals.Instance.OnEnableInput -= OnEnableInput;
            InputSignals.Instance.OnDisableInput -= OnDisableInput;
        }
    }
}