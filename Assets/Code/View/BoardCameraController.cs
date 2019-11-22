using System;
using Cinemachine;
using UnityEngine;

namespace View
{
    public class BoardCameraController:MonoBehaviour
    {
        private CinemachineVirtualCamera _camera;

        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(2))
            {
                
            }
        }
    }
}