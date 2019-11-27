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
        private ClickHandlerScript _clickHandlerScript;

        [OdinSerialize] private ViewHandler _viewHandlerReference;

        [OnValueChanged("UpdateView")]
        [SerializeField] private ViewIdentifierScript _identifierPrefab;

        [SerializeField] private bool _canInput;
        [SerializeField] private bool _canOutput = true;

        public ViewIdentifierScript IdentifierPrefab => _identifierPrefab;

        private void Awake()
        {
            _clickHandlerScript = GetComponent<ClickHandlerScript>();
        }

        private void Start()
        {
            _clickHandlerScript.OnHeld += Output;
        }
        
        public bool Input(ViewIdentifierScript dropped)
        {
            if (_canInput)
            {
                _identifierPrefab = dropped;
                UpdateView(_identifierPrefab);

                DidChangeState?.Invoke();
                
                return true;
            }
            
            return false;
        }

        private void Output()
        {
            if (_viewHandlerReference == null) return;
            if (_identifierPrefab == null) return;
            
            var boardItemView = _viewHandlerReference.InsertItemByGuid(_identifierPrefab.Guid);

            var boardItemClickHandler = boardItemView.GetComponent<ClickHandlerScript>();

            _identifierPrefab = null;

            _viewHandlerReference.ViewHandlerData.MouseHandler.NormalClickHandler.SimulateHold(boardItemClickHandler);
            
            ClearContents();
            
            DidChangeState?.Invoke();
        }

        private void UpdateView(ViewIdentifierScript dropped)
        {
            ClearContents();

            var gameObjectCopy = Instantiate(dropped.GetComponentInChildren<Canvas>().gameObject, transform);
            var rectTrans = GetComponent<RectTransform>();

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
    }
}