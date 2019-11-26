using System;
using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model.BoardItemModels
{
    [CreateAssetMenu(order = 2, fileName = "Resource 1", menuName = "Board Item Data/Resource")]
    public class Resource: BoardItemSerializable
    {
        private float _counter = 0f;
        
        [InfoBox("Set to 0 for no expiration time.")]
        [SerializeField] private float _expirationTime;

        public override float Timer => _counter;
        public override float ExpirationTime => _expirationTime;

        private void UpdateTimer(float deltaTime)
        {
            // Check if there is an expiration time
            if (_expirationTime > 0)
            {
                if (_counter >= _expirationTime)
                {
                    OnExpire?.Invoke();
                    return;
                }
                
                _counter += deltaTime;
            }
        }

        public override void Initialize(ITickable gameTime)
        {
            gameTime.OnTick += UpdateTimer;
        }

        public override event Delegates.VoidDelegate OnExpire;
    }
}