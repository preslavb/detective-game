using System.Collections.Generic;
using _Extensions;
using Boo.Lang.Runtime;
using Model.Interfaces;
using UnityEngine;

namespace View.Scripts.MouseHandler
{
    public class DeductionModeClickHandler: ClickHandler
    {
        private BoardItemScript[] _boardItemPair = new BoardItemScript[2];

        public delegate void BoardItemPairDelegate(BoardItemScript[] boardItemScripts);
        
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
                
                return _mouseHandlerReference.NormalClickHandler;
            }

            return this;
        }

        private bool HandleObjectClicked(GameObject objectClicked)
        {
            if (objectClicked.GetComponent<BoardItemScript>() is var boardItemScript && boardItemScript != null)
            {
                if (_boardItemPair[0] == null)
                {
                    _boardItemPair[0] = boardItemScript;
                    return true;
                }
                if (_boardItemPair[1] == null)
                {
                    _boardItemPair[1] = boardItemScript;
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

        public DeductionModeClickHandler(MouseHandler mouseHandler) : base(mouseHandler)
        {
        }
    }
}