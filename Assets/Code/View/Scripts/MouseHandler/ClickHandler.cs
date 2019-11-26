using System;
using Sirenix.Serialization;
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

        public abstract void Entered();
        public abstract void Escaped();

        public abstract IClickHandler HandleClicks(Camera camera);

        private static T _instance;
    }
}