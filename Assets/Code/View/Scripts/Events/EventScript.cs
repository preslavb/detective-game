using Doozy.Engine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using View.Interfaces;

namespace View.Scripts.Events
{
    public class EventScript: BoardItemScript, IExpirable
    {
        private ClickHandlerScript _clickHandlerScript;
        
        [Required]
        [SerializeField]
        private Image _radialCountdown;

        public Image RadialCountdown => _radialCountdown;

        public void UpdateExpirable(float t)
        {
            _radialCountdown.fillAmount = t;
        }
    }
}