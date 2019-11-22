using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model.Interfaces
{
    public interface IBoardItem
    {
        string Name { get; }
    }

    [SerializeField]
    public abstract class BoardItemSerializable: SerializedScriptableObject, IBoardItem
    {
        public abstract string Name { get; }
    }
}