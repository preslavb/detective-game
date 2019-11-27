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
        private UIView _uiView;

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