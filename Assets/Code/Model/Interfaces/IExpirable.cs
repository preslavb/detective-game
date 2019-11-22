using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model.Interfaces
{
    public interface IExpirable
    {
        float Timer { get; }
        float ExpirationTime { get; }

        void Update();
        
        Action OnExpire { get; set; }
    }
}