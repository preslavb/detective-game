using Doozy.Engine;
using Model;
using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Event = Model.BoardItemModels.Event;

namespace View.Scripts
{
    public class EventScript: BoardItemScript
    {
        private ClickHandlerScript _clickHandlerScript;
        
        [Required]
        [SerializeField]
        private Image _radialCountdown;
        
        [Required]
        [SerializeField] 
        private Event _event;

        private void Awake()
        {
            _clickHandlerScript = GetComponent<ClickHandlerScript>();
            _clickHandlerScript.OnPressRelease += OpenRelevantEventDetails;

            if (_event.ExpirationTime == 0)
            {
                _radialCountdown.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            _event?.Update();
            _radialCountdown.fillAmount = 1 - (_event.Timer / _event.ExpirationTime);
        }

        private void OpenRelevantEventDetails()
        {
            ConstantAccess.Instance.Board.GetComponent<BoardItemSpawner>().OpenEventDetails(_event);
            GameEventMessage.SendEvent("Open Event Details");
        }

        public override BoardItemSerializable BoardItem => _event;
    }
}