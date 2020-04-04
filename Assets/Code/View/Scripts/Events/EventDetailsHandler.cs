using Doozy.Engine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace View.Scripts.Events
{
    public class EventDetailsHandler: MonoBehaviour
    {
        public delegate void EventCompletionDelegate(EventDetailsScript eventCompleted);
        
        [SerializeField] 
        [SceneObjectsOnly]
        private Transform _detailsRoot;

        [SerializeField] 
        [SceneObjectsOnly] 
        private Camera _mainCamera;

        private EventDetailsScript _currentScreenOpen;

        [SerializeField]
        private string _gameEventMessage;

        public void ShowDetailsForEvent(EventDetailsScript prefabToUse)
        {
            if (_currentScreenOpen == null)
            {
                var currentScreenOpenGameObject = Instantiate(prefabToUse.gameObject, _detailsRoot);
                
                _currentScreenOpen = currentScreenOpenGameObject.GetComponent<EventDetailsScript>();
                _currentScreenOpen.Initialize(prefabToUse.Guid);

                _currentScreenOpen.Destroyed += OnEventCompleted;
                
                GameEventMessage.SendEvent(_gameEventMessage);
            }
            else
            {
                Debug.LogError("Already have an event screen open");
            }
        }

        private void OnEventCompleted()
        {
            EventCompleted?.Invoke(_currentScreenOpen);
            Destroy(_currentScreenOpen.gameObject);
        }

        public event EventCompletionDelegate EventCompleted;
    }
}