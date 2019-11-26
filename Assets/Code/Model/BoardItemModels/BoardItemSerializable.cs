using System;
using Model.Interfaces;
using Sirenix.OdinInspector;

namespace Model.BoardItemModels
{
    public abstract class BoardItemSerializable: SerializedScriptableObject, IBoardItem, IExpirable
    {
        public virtual string Name => name;
        
        public virtual float Timer { get; }
        public virtual float ExpirationTime { get; }
        public abstract void Initialize(ITickable gameTime);
        
        
        public virtual Action<float> OnTimerChange { get; set; }

        public virtual event Delegates.VoidDelegate OnExpire;
    }
}