using UnityEngine;

namespace View.Scripts.MouseHandler
{
    public class NormalClickHandler: ClickHandler<NormalClickHandler>, IClickHandler
    {
        private ClickHandlerScript _currentlyHandled;
        
        public override IClickHandler HandleClicks(Camera camera)
        {
            // Just clicked
            if (Input.GetMouseButtonDown(0))
            {
                if (GetElementBeneathMouse(camera, out var result) && result.GetComponent<ClickHandlerScript>() is var _clickHandlerScript != null)
                {
                    _currentlyHandled = _clickHandlerScript;
                    _currentlyHandled.Pressed();
                }
                
                return Instance;
            }
            
            // Just released
            if (Input.GetMouseButtonUp(0))
            {
                if (_currentlyHandled != null)
                {
                    _currentlyHandled.Released();
                    _currentlyHandled = null;
                }
                
                return Instance;
            }
            
            // Clicked right mouse button, so go to right mouse handler
            if (Input.GetMouseButtonDown(1))
            {
                if (_currentlyHandled)
                {
                    _currentlyHandled.Canceled();
                    _currentlyHandled = null;
                    return Instance;
                }
                
                return DeductionModeClickHandler.Instance;
            }

            return Instance;
        }
    }
}