using Model;
using Model.Interfaces;
using UnityEngine;

namespace View.Scripts
{
    [RequireComponent(typeof(ClickHandlerScript))]
    public abstract class BoardItemScript: MonoBehaviour
    {
        public abstract BoardItemSerializable BoardItem { get; }
    }
}