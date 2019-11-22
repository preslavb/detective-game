using Model.Interfaces;
using UnityEngine;

namespace View.Scripts
{
    public class SlotScript:MonoBehaviour
    {
        // The board item currently in the slot
        private BoardItemSerializable _boardItemInSlot;
        
        [SerializeField] private bool _canInput;
        [SerializeField] private bool _canOutput;
        
        public bool Input(GameObject dropped)
        {
            // Check if the input is correct
            if (dropped.GetComponent<BoardItemScript>() is var boardItemScript && boardItemScript != null)
            {
                _boardItemInSlot = boardItemScript.BoardItem;
                return true;
            }

            return false;
        }
    }
}