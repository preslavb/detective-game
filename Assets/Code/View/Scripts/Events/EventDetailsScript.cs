using System;
using Doozy.Engine.Progress;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.Events;

namespace View.Scripts.Events
{
    [RequireComponent(typeof(UIView))]
    public class EventDetailsScript: MonoBehaviour
    {
        private bool _initialized;
        
        private Guid _guid;
        
        private UIView _uiView;

        public void Initialize(Guid guid)
        {
            if (_initialized)
            {
                Debug.LogError("Object already initialized. Make sure this is always only called once for an object", this);
                return;
            }

            _guid = guid;
        }

        private void Awake()
        {
            _uiView = GetComponent<UIView>();
        }

        private void Start()
        {
            _uiView.HideBehavior.OnFinished.Action += o => OnHide();
        }

        public void OnHide()
        {
            Destroyed?.Invoke();
        }

        public event Delegates.VoidDelegate Destroyed;
    }
}