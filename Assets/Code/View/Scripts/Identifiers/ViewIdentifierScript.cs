using System;
using UnityEngine;

namespace View.Scripts.Identifiers
{
    [RequireComponent(typeof(ClickHandlerScript))]
    public class ViewIdentifierScript: MonoBehaviour
    {
        [NonSerialized]
        private bool _initialized;
        private Guid _guid;

        public Guid Guid => _guid;

        public Func<string> GetName;
        public Func<string> GetTypeName;

        public void Initialize(Guid guid)
        {
            if (_initialized)
            {
                Debug.LogError("Already Initialized", this);
                return;
            }

            _initialized = true;
            _guid = guid;
        }

        private void OnMouseEnter()
        {
            TooltipScript.Instance.ShowTooltip($"{GetName?.Invoke() ?? "Unknown Name"}\n{GetTypeName?.Invoke() ?? "Unknown Type"}");
        }

        private void OnMouseExit()
        {
            TooltipScript.Instance.HideTooltip();
        }
    }
}