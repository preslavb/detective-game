using System;
using Model.Interfaces;
using Sirenix.OdinInspector;

namespace Model
{
    public abstract class BoardItemSerializable: SerializedScriptableObject, IBoardItem
    {
        public virtual string Name => name;
        public abstract void Update();
    }
}