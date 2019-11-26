using Doozy.Engine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace View.Scripts.Events
{
    public class EventDetailsHandler: MonoBehaviour
    {
        [SerializeField] 
        [SceneObjectsOnly]
        private Transform _detailsRoot;

        [SerializeField] 
        [SceneObjectsOnly] 
        private Camera _mainCamera;

        private GameObject _currentScreenOpen;

        [SerializeField]
        private string _gameEventMessage;

        public void ShowDetailsForEvent(GameObject prefabToUse)
        {
            if (_currentScreenOpen == null)
            {
                _currentScreenOpen = Instantiate(prefabToUse, _detailsRoot);
                GameEventMessage.SendEvent(_gameEventMessage);
            }
            else
            {
                Debug.LogError("Already have an event screen open");
            }
        }
    }
}