using System;
using UnityEngine;

namespace View.Scripts.MouseHandler
{
    public class MouseHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private IClickHandler _clickHandler = NormalClickHandler.Instance;

        // Update is called once per frame
        void Update()
        {
            _clickHandler = _clickHandler.HandleClicks(_camera);
        }
    }
}
