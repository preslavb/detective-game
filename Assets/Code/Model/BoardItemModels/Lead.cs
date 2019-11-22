using System;
using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model.BoardItemModels
{
    [CreateAssetMenu(order = 1, fileName = "Lead 1", menuName = "Board Item Data/Lead")]
    public class Lead: BoardItemData, IExpirable
    {
        private float _counter = 0f;
        
        [InfoBox("Set to 0 for no expiration time.")]
        [SerializeField] private float _expirationTime;

        public float Timer => _counter;
        
        public float ExpirationTime => _expirationTime;

        public override void Update()
        {
            base.Update();
            
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

        public Action OnExpire { get; set; }
    }
}