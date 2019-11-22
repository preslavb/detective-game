using System;
using Cinemachine;
using UnityEngine;

namespace View
{
    public class BoardCameraController:MonoBehaviour
    {
        private Vector3 _initialMousePosition;
        private Plane _planeToMoveOn;
        
        [SerializeField]
        private Camera _camera;

        private void Awake()
        {
            var board = ConstantAccess.Instance.Board;
            _planeToMoveOn = new Plane(board.transform.forward, board.transform.position);
        }

        private void Update()
        {
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

                    var difference = _camera.WorldToViewportPoint(hitPoint) - _camera.WorldToViewportPoint(_initialMousePosition);
                    
                    Debug.Log(difference);
                    
                    transform.Translate(difference);
                }
            }
        }
    }
}