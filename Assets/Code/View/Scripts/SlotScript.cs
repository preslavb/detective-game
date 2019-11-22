using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using View.Scripts.MouseHandler;

namespace View.Scripts
{
    [RequireComponent(typeof(ClickHandlerScript))]
    public class SlotScript:MonoBehaviour
    {
        // The board item currently in the slot
        [ShowInInspector]
        [ShowIf("_showInInspector")]
        private BoardItemSerializable _boardItemInSlot;

        private ClickHandlerScript _clickHandlerScript;
        
        [SerializeField] private bool _canInput;
        [SerializeField] private bool _canOutput;

        private void Awake()
        {
            _clickHandlerScript = GetComponent<ClickHandlerScript>();
        }

        private void Start()
        {
            _clickHandlerScript.OnHeld += Output;
        }

#if UNITY_EDITOR
        private bool _showInInspector => _canInput || _canOutput;
        #endif
        
        public bool Input(GameObject dropped)
        {
            if (!_canInput) return false;
            
            // Check if the input is correct
            if (dropped.GetComponent<BoardItemScript>() is var boardItemScript && boardItemScript != null)
            {
                _boardItemInSlot = boardItemScript.BoardItem;
                
                UpdateView(dropped);
                
                return true;
            }

            return false;
        }

        private void UpdateView(GameObject dropped)
        {
            var gameObjectCopy = Instantiate(dropped.GetComponentInChildren<Canvas>().gameObject, transform);
            var rectTrans = GetComponent<RectTransform>();
            
            gameObjectCopy.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, rectTrans.rect.width);
            gameObjectCopy.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rectTrans.rect.height);
        }

        private void Output()
        {
            if (_boardItemInSlot == null) return;
            
            // Get the reference to the board manager
            var boardItemSpawner = ConstantAccess.Instance.Board.GetComponent<BoardItemSpawner>();
            
            var spawnedItem = boardItemSpawner.Spawn(_boardItemInSlot);
            NormalClickHandler.Instance.SimulateHold(spawnedItem.GetComponent<ClickHandlerScript>());
            
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
    }
}