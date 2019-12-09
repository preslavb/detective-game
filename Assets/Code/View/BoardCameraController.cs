using System;
using Cinemachine;
using UnityEngine;
using _Extensions;
using UnityEngine.EventSystems;

namespace View
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class BoardCameraController:MonoBehaviour
    {
        [SerializeField]
        [Range(0.5f, 1.5f)]
        private float _cameraDamping = 1;
        
        private Vector3 _initialMousePosition;
        private Plane _planeToMoveOn;
        
        [SerializeField]
        private Camera _camera;

        [SerializeField] 
        private GameObject _board;

        private CinemachineVirtualCamera _virtualCamera;

        private void Awake()
        {
            _planeToMoveOn = new Plane(_board.transform.forward, _board.transform.position);
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (CinemachineCore.Instance.IsLive(_virtualCamera) && !CinemachineCore.Instance.GetActiveBrain(0).IsBlending)
                transform.position = _camera.transform.position;
            
            if (Input.GetMouseButtonDown(2))
            {
                // Create a ray from the mousePosition
                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (_planeToMoveOn.Raycast(ray, out var distance))
                {
                    var hitPoint = ray.GetPoint(distance);

                    _initialMousePosition = hitPoint;
                }
            }

            if (Input.GetMouseButton(2))
            {
                // Create a ray from the mousePosition
                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (_planeToMoveOn.Raycast(ray, out var distance))
                {
                    var hitPoint = ray.GetPoint(distance);

                    var difference = (_camera.WorldToViewportPoint(hitPoint) - _camera.WorldToViewportPoint(_initialMousePosition)) * distance * _cameraDamping;

                    // Update the virtual camera's position
                    transform.Translate(-difference.With(z: 0));
                    
                    /*
                     * This will prompt the actual scene camera to move as well, but that one is confined to the board space.
                     * To fix this, we need to reset the virtual camera's position at the start of next frame
                     */
                }
            }

            if (Input.mouseScrollDelta.y != 0 && !EventSystem.current.IsPointerOverGameObject())
            {
                transform.Translate(new Vector3().With(z: Input.mouseScrollDelta.y));
            }
        }
    }
}