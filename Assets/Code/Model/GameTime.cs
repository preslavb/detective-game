using System;
using Model.Interfaces;
using UnityEngine;

namespace Model
{
    public class GameTime: ITickable
    {
        private float _deltaTime = 0;
        
        public float DeltaTime => _deltaTime;
        public float TimeScale { get; set; } = 1;

        public void Tick(float systemDeltaTime)
        {
            _deltaTime = systemDeltaTime * TimeScale;
            
            OnTick?.Invoke(_deltaTime);
        }

        public Action<float> OnTick { get; set; }
    }
}