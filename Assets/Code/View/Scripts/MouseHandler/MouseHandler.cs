using System;
using UnityEngine;

namespace View.Scripts.MouseHandler
{
    public class MouseHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private YarnControllerScript _lineRendererPrefab;

        public NormalClickHandler NormalClickHandler { get; }
        public DeductionModeClickHandler DeductionModeClickHandler { get; private set; }

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
            _clickHandler = NormalClickHandler;
        }

        private void Awake()
        {
            DeductionModeClickHandler = new DeductionModeClickHandler(this, _lineRendererPrefab);
        }

        void Update()
        {
            ClickHandler = _clickHandler.HandleClicks(_camera);
        }
    }
}
