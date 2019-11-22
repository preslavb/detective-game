using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Event = Model.BoardItemModels.Event;

namespace View.Scripts
{
    public class EventScript: BoardItemScript
    {
        [Required]
        [SerializeField]
        private Image _radialCountdown;
        
        [Required]
        [SerializeField] 
        private Event _event;

        private void Update()
        {
            _event?.Update();
            _radialCountdown.fillAmount = 1 - (_event.Timer / _event.ExpirationTime);
        }

        public override BoardItemSerializable BoardItem => _event;
    }
}