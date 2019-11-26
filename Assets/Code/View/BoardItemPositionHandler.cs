using System.Linq;
using _Extensions;
using UnityEngine;
using View.Scripts;

namespace View
{
    [RequireComponent(typeof(ClickHandlerScript))]
    public class BoardItemPositionHandler: MonoBehaviour
    {
        //______________ PRIVATE STATE  ______________
        private Camera _camera;
        private Transform _board;
        
        private static float _boardOffset = -0.115f;
        
        private ClickHandlerScript _clickHandlerScript;

        private Plane _movementPlane;

        private bool _moveWithMouse = false;

        private Vector3 _startPosition;

        public void Initialize(Camera camera, Transform board)
        {
            _camera = camera;
            _board = board;
            
            _clickHandlerScript = GetComponent<ClickHandlerScript>();

            // Initialize the plane
            _movementPlane = new Plane(_board.forward, _board.position);

            _clickHandlerScript.OnHeld += StartMoving;
            _clickHandlerScript.OnRelease += Released;
            _clickHandlerScript.OnCanceled += ResetPosition;
        }

        private void StartMoving()
        {
            _moveWithMouse = true;
            _startPosition = transform.position;
            
            Update();
            
            // Bring the canvas forward
            GetComponentInChildren<Canvas>().sortingOrder = 100;
        }

        private void ResetPosition()
        {
            transform.position = _startPosition;
            _moveWithMouse = false;
            
            // Bring the canvas forward
            GetComponentInChildren<Canvas>().sortingOrder = 0;
        }

        private void Released()
        {
            // First, check if we were released on a slot
            if (_camera.GetElementBeneathMouse(out var elements, true))
            {
                SlotScript slotElement = null;
                
                if (elements.Any(element => (slotElement = element.GetComponent<SlotScript>()) != null) && slotElement.Input(gameObject))
                {
                    Destroy(gameObject);
                }
            }
            
            _moveWithMouse = false;
                
            // Bring the canvas forward
            GetComponentInChildren<Canvas>().sortingOrder = 0;
        }

        private void Update()
        {
            if (_moveWithMouse)
            {
                // Create a ray from the mousePosition
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                float distance;
                
                if (_movementPlane.Raycast(ray, out distance)){
                    var hitPoint = ray.GetPoint(distance);

                    transform.position = hitPoint;
                    transform.Translate(new Vector3(0, 0, _boardOffset), _board.transform);
                }
            }
        }
    }
}