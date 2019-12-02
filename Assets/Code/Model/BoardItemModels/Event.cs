using System;
using System.Collections.Generic;
using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using View;

namespace Model.BoardItemModels
{
    [CreateAssetMenu(order = 5, fileName = "Event 1", menuName = "Board Item Data/Event")]
    public class Event: BoardItemSerializable
    {
        [NonSerialized]
        private float _counter = 0;

        [SerializeField]
        private float _expirationTime;
        
        [SerializeField]
        private BoardItemSerializable[] _eventDecisions;

        [SerializeField]
        [Required]
        [InlineProperty]
        [ShowIf("ShowDefaultDecision")]
        [InfoBox("If the player does not respond to the event, this will spawn.")]
        private BoardItemSerializable _defaultDecision;

        [SerializeField] private bool _destroyOnCompletion;

        public bool DestroyOnCompletion => _destroyOnCompletion;

        public int DecisionsCount => _eventDecisions.Length;

        public BoardItemSerializable[] EventDecisions => _eventDecisions;

        public override float Timer => _counter;
        public override float ExpirationTime => _expirationTime;

        private void UpdateTimer(float deltaTime)
        {
            if (_expirationTime > 0)
            {
                if (_counter >= _expirationTime)
                {
                    OnExpire?.Invoke();
                }
                
                _counter += deltaTime;
                OnTimerChange?.Invoke(1 - (_counter/_expirationTime));
            }
        }

        public override void Initialize(ITickable gameTime)
        {
            gameTime.OnTick += UpdateTimer;
        }

        public override event Delegates.VoidDelegate OnExpire;
        
        #if UNITY_EDITOR
        private bool ShowDefaultDecision => _expirationTime > 0;
        #endif
    }
}