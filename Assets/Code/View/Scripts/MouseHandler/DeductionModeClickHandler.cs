using System;
using _Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using View.Scripts.Identifiers;

namespace View.Scripts.MouseHandler
{
    public class DeductionModeClickHandler: ClickHandler
    {
        private const float YarnOffset = 0.3f;
        
        private ViewIdentifierScript[] _boardItemPair = new ViewIdentifierScript[2];

        public YarnControllerScript _yarnControllerPrefab;

        public delegate bool BoardItemPairDelegate(ViewIdentifierScript[] boardItemScripts);

        private YarnControllerScript _yarnController;
        
        public event BoardItemPairDelegate OnCreatedAPair; 
        
        public override void Entered()
        {
        }

        public override void Escaped()
        {
            ClearBoardItemPair();
        }

        public override IClickHandler HandleClicks(Camera camera)
        {
            if (_boardItemPair[0] != null)
            {
                var mouseHit = camera.GetPointBeneathMouse();
                _yarnController.LineRenderer.SetPosition(1, mouseHit.point + (mouseHit.normal * YarnOffset));
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Get the object below the mouse
                camera.GetElementBeneathMouse(out var results);

                foreach (var result in results)
                {
                    if (HandleObjectClicked(result)) return this;
                }
            }
            
            
            if (Input.GetMouseButtonUp(1))
            {
                // TODO: Clear all pins 
                GameObject.Destroy(_yarnController?.gameObject);
                
                return _mouseHandlerReference.NormalClickHandler;
            }

            return this;
        }

        private bool HandleObjectClicked(RaycastResult result)
        {
            if (result.gameObject.GetComponent<ViewIdentifierScript>() is var viewIdentifierScript && viewIdentifierScript != null)
            {
                if (_boardItemPair[0] == null)
                {
                    _yarnController = GameObject.Instantiate(_yarnControllerPrefab);

                    _yarnController.LineRenderer.positionCount = 2;
                    _yarnController.LineRenderer.useWorldSpace = true;

                    var newObject = GameObject.Instantiate(new GameObject(), result.gameObject.transform);
                    newObject.transform.position = result.worldPosition + (result.worldNormal * YarnOffset);
                    
                    _yarnController.LineRenderer.SetPosition(0, newObject.transform.position);
                    _yarnController.SetItem(0, newObject.transform);
                    
                    _boardItemPair[0] = viewIdentifierScript;
                    return true;
                }
                if (_boardItemPair[1] == null)
                {
                    var newObject = GameObject.Instantiate(new GameObject(), result.gameObject.transform);
                    newObject.transform.position = result.worldPosition + (result.worldNormal * YarnOffset);
                    
                    _yarnController.SetItem(1, newObject.transform);
                    
                    _boardItemPair[1] = viewIdentifierScript;

                    if (!NotifyPairCreation())
                    {
                        GameObject.Destroy(_yarnController?.gameObject);
                    }
                    
                    _yarnController = null;
                    return true;
                }
                
                throw new Exception("Deduction board item pairs not handled properly.");
            }

            // If the object was not handled, route back and try another one in the click stack
            return false;
        }

        private bool NotifyPairCreation()
        {
            // DO the notification
            var result = OnCreatedAPair?.Invoke(_boardItemPair);
            
            ClearBoardItemPair();

            return result ?? false;
        }

        private void ClearBoardItemPair()
        {
            _boardItemPair[0] = null;
            _boardItemPair[1] = null;
        }

        public DeductionModeClickHandler(MouseHandler mouseHandler, YarnControllerScript yarnControllerPrefab) : base(mouseHandler)
        {
            _yarnControllerPrefab = yarnControllerPrefab;
        }
    }
}