using System;
using UnityEngine;

namespace View.Scripts.Identifiers
{
    [RequireComponent(typeof(ClickHandlerScript))]
    public class ViewIdentifierScript: MonoBehaviour
    {
        private bool _initialized;
        private Guid _guid;

        public Guid Guid => _guid;

        public void Initialize(Guid guid)
        {
            if (_initialized)
            {
                Debug.LogError("Already Initialized", this);
                return;
            }

            _guid = guid;
        }
    }
}