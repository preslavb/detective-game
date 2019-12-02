using System;
using Sirenix.OdinInspector;
using UnityEngine;
using View.Interfaces;
using View.Scripts.Events;
using View.Scripts.Identifiers;

namespace View.ViewDataClasses
{
    public class EventViewData: IBoardItemViewProperties
    {
        [AssetsOnly]
        [SerializeField] 
        private ViewIdentifierScript _viewIdentifierScript;

        [AssetsOnly] 
        [SerializeField] 
        private EventDetailsScript _eventDetailsPrefab;

        [SerializeField] 
        private Vector2? _startingPosition;

        public EventDetailsScript EventDetailsPrefab => _eventDetailsPrefab;
        public Vector2? StartingPosition => _startingPosition;
        
        public void Initialize(Guid guid)
        {
            _viewIdentifierScript.Initialize(guid);
            _eventDetailsPrefab.Initialize(guid);
        }

        public ViewIdentifierScript ViewIdentifierScript => _viewIdentifierScript;
    }
}