using System;
using Cinemachine;
using Doozy.Engine;
using UnityEngine;

namespace View
{
    public class StartMenuScreenController: MonoBehaviour
    {
        [SerializeField] 
        private CinemachineVirtualCamera _mainMenuCamera;

        [SerializeField] 
        private string _eventToSend;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _mainMenuCamera.enabled = false;
                GameEventMessage.SendEvent(_eventToSend);
                Destroy(this);
            }
        }
    }
}