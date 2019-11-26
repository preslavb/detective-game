using System;
using UnityEngine;

namespace View.Scripts.MouseHandler
{
    public class MouseHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private IClickHandler _clickHandler = NormalClickHandler.Instance;

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

        // Update is called once per frame
        void Update()
        {
            ClickHandler = _clickHandler.HandleClicks(_camera);
        }
    }
}
