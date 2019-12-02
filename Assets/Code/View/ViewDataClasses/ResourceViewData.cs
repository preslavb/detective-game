using System;
using Sirenix.OdinInspector;
using UnityEngine;
using View.Interfaces;
using View.Scripts.Identifiers;

namespace View.ViewDataClasses
{
    public class ResourceViewData: IBoardItemViewProperties
    {
        [AssetsOnly]
        [SerializeField] 
        private ViewIdentifierScript _viewIdentifierScript;
        
        [SerializeField] 
        private Vector2? _startingPosition;

        public ViewIdentifierScript ViewIdentifierScript => _viewIdentifierScript;
        public Vector2? StartingPosition => _startingPosition;
        
        public void Initialize(Guid guid)
        {
            _viewIdentifierScript.Initialize(guid);
        }
    }
}