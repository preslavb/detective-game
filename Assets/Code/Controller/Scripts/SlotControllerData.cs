using Model.BoardItemModels;
using UnityEngine;
using View.Scripts.Events;

namespace Controller.Scripts
{
    [RequireComponent(typeof(SlotScript))]
    internal class SlotControllerData: MonoBehaviour
    {
        [SerializeField] 
        private BoardItemSerializable _boardItemInSlot;
    }
}