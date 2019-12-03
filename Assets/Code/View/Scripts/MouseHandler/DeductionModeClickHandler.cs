using System.Collections.Generic;
using _Extensions;
using Boo.Lang.Runtime;
using Model.Interfaces;
using UnityEngine;
using View.Scripts.Identifiers;

namespace View.Scripts.MouseHandler
{
    public class DeductionModeClickHandler: ClickHandler
    {
        private ViewIdentifierScript[] _boardItemPair = new ViewIdentifierScript[2];

        public LineRenderer _lineRendererPrefab;

        public delegate void BoardItemPairDelegate(ViewIdentifierScript[] boardItemScripts);

        private LineRenderer _lineRender;
        
        public event BoardItemPairDelegate OnCreatedAPair; 
        
        public override void Entered()
        {
            Debug.Log("Entered Deduction");
        }

        public override void Escaped()
        {
            ClearBoardItemPair();
            Debug.Log("Exited Deduction");
        }

        public override IClickHandler HandleClicks(Camera camera)
        {
            if (_boardItemPair[0] != null)
            {
                _lineRender.SetPosition(1, camera.GetPointBeneathMouse());
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Get the object below the mouse
                camera.GetElementBeneathMouse(out var results);

                foreach (var gameObject in results)
                {
                    if (HandleObjectClicked(gameObject)) return this;
                }
            }
            
            
            if (Input.GetMouseButtonUp(1))
            {
                // TODO: Clear all pins 
                GameObject.Destroy(_lineRender.gameObject);
                
                return _mouseHandlerReference.NormalClickHandler;
            }

            return this;
        }

        private bool HandleObjectClicked(GameObject objectClicked)
        {
            if (objectClicked.GetComponent<ViewIdentifierScript>() is var viewIdentifierScript && viewIdentifierScript != null)
            {
                if (_boardItemPair[0] == null)
                {
                    _lineRender = GameObject.Instantiate(_lineRendererPrefab);

                    _lineRender.positionCount = 2;
                    _lineRender.useWorldSpace = true;
                    _lineRender.SetPosition(0, viewIdentifierScript.transform.position);
                    _lineRender.SetPosition(1, viewIdentifierScript.transform.position);
                    
                    _boardItemPair[0] = viewIdentifierScript;
                    return true;
                }
                if (_boardItemPair[1] == null)
                {
                    _boardItemPair[1] = viewIdentifierScript;
                    NotifyPairCreation();
                    return true;
                }
                
                throw new RuntimeException("Deduction board item pairs not handled properly.");
            }

            // If the object was not handled, route back and try another one in the click stack
            return false;
        }

        private void NotifyPairCreation()
        {
            // DO the notification
            OnCreatedAPair?.Invoke(_boardItemPair);
            
            ClearBoardItemPair();
        }

        private void ClearBoardItemPair()
        {
            _boardItemPair[0] = null;
            _boardItemPair[1] = null;
        }

        public DeductionModeClickHandler(MouseHandler mouseHandler, LineRenderer lineRendererPrefab) : base(mouseHandler)
        {
            _lineRendererPrefab = lineRendererPrefab;
        }
    }
}