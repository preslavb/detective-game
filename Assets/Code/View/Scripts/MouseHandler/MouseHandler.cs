using System;
using UnityEngine;

namespace View.Scripts.MouseHandler
{
    public class MouseHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        public NormalClickHandler NormalClickHandler { get; }
        public DeductionModeClickHandler DeductionModeClickHandler { get; }

        private IClickHandler _clickHandler;

        public IClickHandler ClickHandler
        {
            private get { return _clickHandler; }
            
            set
            {
                if (_clickHandler != value)
                {
                    _clickHandler.Escaped();
                    _clickHandler = value;
                    _clickHandler.Entered();
                }
            }
        }

        // WARNING: this is a measured risk, and ctors shouldn't usually be used with MonoBehaviours
        public MouseHandler()
        {
            NormalClickHandler = new NormalClickHandler(this);
            DeductionModeClickHandler = new DeductionModeClickHandler(this);
            _clickHandler = NormalClickHandler;
        }

        void Update()
        {
            ClickHandler = _clickHandler.HandleClicks(_camera);
        }
    }
}
