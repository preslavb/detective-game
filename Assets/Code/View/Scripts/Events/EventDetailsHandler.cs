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

        public void ShowDetailsForEvent(EventDetailsScript prefabToUse)
        {
            if (_currentScreenOpen == null)
            {
                _currentScreenOpen = Instantiate(prefabToUse.gameObject, _detailsRoot);
                _currentScreenOpen.GetComponent<EventDetailsScript>().Destroyed += ClearHandledEventDetailsOnDestroy;
                
                GameEventMessage.SendEvent(_gameEventMessage);
            }
            else
            {
                Debug.LogError("Already have an event screen open");
            }
        }

        private void ClearHandledEventDetailsOnDestroy()
        {
            Destroy(_currentScreenOpen);
        }
    }
}