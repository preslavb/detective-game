using Model.Interfaces;
using UnityEngine;

namespace View.Scripts
{
    public abstract class BoardItemScript: MonoBehaviour
    {
        public abstract BoardItemSerializable BoardItem { get; }
    }
}