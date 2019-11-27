using System;
using UnityEngine;

namespace View.Scripts.Identifiers
{
    [RequireComponent(typeof(ClickHandlerScript))]
    public class ViewIdentifierScript: MonoBehaviour
    {
        public Guid Guid { get; set; }
    }
}