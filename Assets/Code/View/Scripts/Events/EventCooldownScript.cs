using Doozy.Engine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using View.Interfaces;

namespace View.Scripts.Events
{
    public class EventCooldownScript: BoardItemScript, IExpirable
    {
        private ClickHandlerScript _clickHandlerScript;
        
        [Required]
        [SerializeField]
        private Image _radialCountdown;

        private bool _loggedExpiration = false;

        public Image RadialCountdown => _radialCountdown;

        public void UpdateExpirable(float t)
        {
            _radialCountdown.fillAmount = t;

            if (t <= 0 && !_loggedExpiration)
            {
                DidExpire?.Invoke();
                _loggedExpiration = true;
            }
        }

        public event Delegates.VoidDelegate DidExpire;
    }
}