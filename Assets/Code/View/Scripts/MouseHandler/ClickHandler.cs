using System;
using Sirenix.Serialization;
using UnityEngine;

namespace View.Scripts.MouseHandler
{
    [Serializable]
    public abstract class ClickHandler: IClickHandler
    {
        public ClickHandler(MouseHandler mouseHandler)
        {
            _mouseHandlerReference = mouseHandler;
        }
        
        protected MouseHandler _mouseHandlerReference;

        public abstract void Entered();
        public abstract void Escaped();

        public abstract IClickHandler HandleClicks(Camera camera);
    }
}