using System;
using Sirenix.Serialization;
using UnityEngine;

namespace View
{
    public class ClickHandlerScript: MonoBehaviour
    {
        public delegate void VoidDelegate();
        
        // ______________ PUBLIC API _________________
        public void Pressed()
        {
            _isMousePressed = true;
            _isMouseHeld = false;
            _timeElapsed = 0;
            _initialMousePos = Input.mousePosition;
            
            OnClick?.Invoke();
        }

        public void PressHold()
        {
            _isMousePressed = true;
            _isMouseHeld = false;
            _timeElapsed = _timeForHeld;
            _initialMousePos = Input.mousePosition;

            OnClick?.Invoke();
            OnHeld?.Invoke();
        }

        public void Released()
        {
            if (_timeElapsed < _timeForHeld && !_isMouseHeld)
            {
                OnPressRelease?.Invoke();
            }
            
            OnRelease?.Invoke();
            
            _isMousePressed = false;
            _isMouseHeld = false;
            _timeElapsed = 0;
        }

        public void Canceled()
        {
            _isMousePressed = false;
            _isMouseHeld = false;
            _timeElapsed = 0;
            
            OnCanceled?.Invoke();
        }
        
        public event VoidDelegate OnClick;
        public event VoidDelegate OnPressRelease;
        public event VoidDelegate OnRelease;
        public event VoidDelegate OnHeld;
        public event VoidDelegate OnCanceled;

        // ___________PRIVATE STATE___________________
        private static float _timeForHeld = 0.2f;
        private static float _draggedDeltaThreshold = 50f;
    
        private bool _isMousePressed;
        private bool _isMouseHeld;
        
        private Vector2 _initialMousePos;
        private Vector2 _mouseDelta;
        
        private float _timeElapsed;

        private void Update()
        {
            if (_isMousePressed)
            {
                _timeElapsed += Time.deltaTime;
                _mouseDelta = (Vector2)Input.mousePosition - _initialMousePos;

                if (!_isMouseHeld && (_timeElapsed >= _timeForHeld || _mouseDelta.magnitude > _draggedDeltaThreshold))
                {
                    _isMouseHeld = true;
                    OnHeld?.Invoke();
                }
            }
        }
    }
}