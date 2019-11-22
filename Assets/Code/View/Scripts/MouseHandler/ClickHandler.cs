using System;
using UnityEngine;

namespace View.Scripts.MouseHandler
{
    [Serializable]
    public abstract class ClickHandler<T> where T: ClickHandler<T>, IClickHandler, new()
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        public abstract IClickHandler HandleClicks(Camera camera);

        private static T _instance;

        protected ClickHandler() {}
        
        protected bool GetElementBeneathMouse(Camera camera, out GameObject result)
        {
            RaycastHit raycastResult;

            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out raycastResult, 30))
            {
                result = raycastResult.collider.gameObject;
                return true;
            }

            result = null;
            return false;
        }
    }
}