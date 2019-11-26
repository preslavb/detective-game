using System.Linq;
using UnityEngine;
using _Extensions;

namespace View.Scripts.MouseHandler
{
    public class NormalClickHandler: ClickHandler<NormalClickHandler>, IClickHandler
    {
        private ClickHandlerScript _currentlyHandled;

        public void SimulateClick(ClickHandlerScript clickHandlerScript)
        {
            _currentlyHandled = clickHandlerScript;
            _currentlyHandled.Pressed();
        }

        public void SimulateHold(ClickHandlerScript clickHandlerScript)
        {
            _currentlyHandled = clickHandlerScript;
            _currentlyHandled.PressHold();
        }

        public override void Entered()
        {
            Debug.Log("Entered Normal Click Mode");
        }

        public override void Escaped()
        {
            Debug.Log("Exited Normal Click Mode");
        }

        public override IClickHandler HandleClicks(Camera camera)
        {
            // Just clicked
            if (Input.GetMouseButtonDown(0))
            {
                ClickHandlerScript clickHandlerScript = null;
                
                if (camera.GetElementBeneathMouse(out var results, true) && 
                    results.Any(go => (clickHandlerScript = go.GetComponent<ClickHandlerScript>()) != null ) &&
                    clickHandlerScript != null
                ) {
                    _currentlyHandled = clickHandlerScript;
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