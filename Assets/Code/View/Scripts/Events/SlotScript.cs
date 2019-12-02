using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using View.Scripts.Identifiers;
using View.Scripts.MouseHandler;

namespace View.Scripts.Events
{
    [RequireComponent(typeof(ClickHandlerScript))]
    public class SlotScript:SerializedMonoBehaviour
    {
        public delegate bool GetExternalRequirementsDelegate(SlotScript slotScriptToCheck, ViewIdentifierScript scriptDropped); 
        
        private ClickHandlerScript _clickHandlerScript;

        [OdinSerialize] private ViewHandler _viewHandlerReference;

        [OnValueChanged("UpdateView")]
        [SerializeField] private ViewIdentifierScript _identifierPrefab;

        [SerializeField] private bool _canInput;
        [SerializeField] private bool _canOutput = true;

        public ViewIdentifierScript IdentifierPrefab => _identifierPrefab;

        private Guid _currentItemInSlotGuid;

        public Guid CurrentItemInSlotGuid => _currentItemInSlotGuid;

        private void Awake()
        {
            _clickHandlerScript = GetComponent<ClickHandlerScript>();
            _currentItemInSlotGuid = _identifierPrefab != null ? _identifierPrefab.Guid : Guid.Empty;
        }

        private void Start()
        {
            _clickHandlerScript.OnHeld += Output;
        }
        
        public bool Input(ViewIdentifierScript dropped)
        {
            if (_canInput && (GetExternalRequirements?.Invoke(this, dropped) ?? true))
            {
                UpdateView(dropped);

                DidChangeState?.Invoke();
                
                return true;
            }
            
            return false;
        }

        private void Output()
        {
            if (_viewHandlerReference == null) return;
            if (_currentItemInSlotGuid == Guid.Empty) return;
            
            var boardItemView = _viewHandlerReference.InsertItemByGuid(_currentItemInSlotGuid);

            var boardItemClickHandler = boardItemView.GetComponent<ClickHandlerScript>();

            _currentItemInSlotGuid = Guid.Empty;

            _viewHandlerReference.ViewHandlerData.MouseHandler.NormalClickHandler.SimulateHold(boardItemClickHandler);
            
            ClearContents();
            
            DidChangeState?.Invoke();
        }

        private void UpdateView(ViewIdentifierScript dropped)
        {
            ClearContents();

            var gameObjectCopy = Instantiate(dropped.GetComponentInChildren<Canvas>().gameObject, transform);
            var rectTrans = GetComponent<RectTransform>();

            _currentItemInSlotGuid = dropped.Guid;

            gameObjectCopy.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, rectTrans.rect.width);
            gameObjectCopy.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rectTrans.rect.height);
        }

        private void ClearContents()
        {
            // Delete all children
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        public event Delegates.VoidDelegate DidChangeState;
        public event GetExternalRequirementsDelegate GetExternalRequirements;
    }
}