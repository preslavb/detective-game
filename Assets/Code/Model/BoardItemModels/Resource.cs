using System;
using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model.BoardItemModels
{
    [CreateAssetMenu(order = 2, fileName = "Resource 1", menuName = "Board Item Data/Resource")]
    public class Resource: BoardItemSerializable, IExpirable
    {
        private float _counter = 0f;
        
        [InfoBox("Set to 0 for no expiration time.")]
        [SerializeField] private float _expirationTime;

        public float Timer => _counter;
        public float ExpirationTime => _expirationTime;

        public override void Update()
        {
            // Check if there is an expiration time
            if (_expirationTime > 0)
            {
                if (_counter >= _expirationTime)
                {
                    OnExpire?.Invoke();
                    return;
                }
                
                _counter += Time.deltaTime;
            }
        }

        public event Delegates.VoidDelegate OnExpire;
    }
}