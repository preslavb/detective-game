using System.Collections.Generic;
using System.Linq;
using Model;
using Model.BoardItemModels;
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
        }
    }
}