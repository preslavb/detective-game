using System.Collections.Generic;
using System.Linq;
using Model;
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
        [SerializeField]
        [ValueDropdown("GetViableDropdownOptions")]
        [OnValueChanged("SpawnInEditor")]
        [ShowIf("_showInInspector")]
        private BoardItemSerializable _boardItemInSlot;

        private ClickHandlerScript _clickHandlerScript;
        
        [SerializeField] private bool _canInput;
        [SerializeField] private bool _canOutput = true;

        [SerializeField]
        private EventDetailsScript _eventDetailsScript;

        private void Awake()
        {
            _clickHandlerScript = GetComponent<ClickHandlerScript>();
        }

        private void Start()
        {
            _clickHandlerScript.OnHeld += Output;
        }
        
        public bool Input(GameObject dropped)
        {
            if (!_canInput) return false;

            // Check if the slot already has an item in it, and if so, output it before inputing the new one
            if (_boardItemInSlot != null) Output();
            
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
            // Delete all children
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
            
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

#if UNITY_EDITOR
        private bool _showInInspector => _canInput || _canOutput;

        private void SpawnInEditor()
        {
            var newValue = _boardItemInSlot;

            if (newValue == null)
            {
                // Delete all children
                foreach (Transform child in transform)
                {
                    DestroyImmediate(child.gameObject);
                }

                return;
            }
            
            var newSpawn = GameObject.FindObjectOfType<BoardItemSpawner>().Spawn(newValue);
            UpdateView(newSpawn);
            
            DestroyImmediate(newSpawn);
        }

        private IEnumerable<BoardItemSerializable> GetViableDropdownOptions()
        {
            return _eventDetailsScript?.Event?.EventDecisions ?? new BoardItemSerializable[0];
        }

        private void OnValidate()
        {
            _eventDetailsScript = GetComponentInParent<EventDetailsScript>();

            if (!_eventDetailsScript?.Event?.EventDecisions?.Contains(_boardItemInSlot) ?? false)
            {
                _boardItemInSlot = null;
                SpawnInEditor();
            }
        }
#endif
    }
}