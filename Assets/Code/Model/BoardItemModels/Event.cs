using System;
using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using View;

namespace Model.BoardItemModels
{
    [CreateAssetMenu(order = 5, fileName = "Event 1", menuName = "Board Item Data/Event")]
    public class Event: BoardItemData, IExpirable
    {
        [NonSerialized]
        private float _counter = 0;

        [SerializeField]
        private float _expirationTime;
        
        [SerializeField]
        [TableList]
        private EventDecision[] _eventDecisions;

        [SerializeField]
        [Required]
        [InlineProperty]
        [InfoBox("If the player does not respond to the event, this will trigger.")]
        private EventDecision _defaultDecision;

        public int DecisionsCount => _eventDecisions.Length;

        public EventDecision GetDecision(int index)
        {
            if (index < _eventDecisions.Length)
            {
                return _eventDecisions[index];
            }

            else
            {
                throw new Exception("Tried to access event decision at index out of bounds");
            }
        }

        public float Timer => _counter;
        public float ExpirationTime => _expirationTime;
        
        public override void Update()
        {
            base.Update();

            if (_expirationTime > 0)
            {
                if (_counter >= _expirationTime)
                {
                    OnExpire?.Invoke();
                }
                
                _counter += GameTime.Instance.DeltaTime;
            }
        }

        public Action OnExpire { get; set; }
    }
}