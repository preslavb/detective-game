using System;
using Sirenix.OdinInspector;
using UnityEngine;
using View.Interfaces;
using View.Scripts.Events;
using View.Scripts.Identifiers;

namespace View.ViewDataClasses
{
    [Serializable]
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

        public EventViewData(ViewIdentifierScript viewIdentifierScript, EventDetailsScript eventDetailsPrefab, Vector2? startingPosition)
        {
            _viewIdentifierScript = viewIdentifierScript;
            _eventDetailsPrefab = eventDetailsPrefab;
            _startingPosition = startingPosition;
        }
        
        public void Initialize(Guid guid)
        {
            if (_viewIdentifierScript != null) _viewIdentifierScript.Initialize(guid);
            if (_eventDetailsPrefab != null) _eventDetailsPrefab.Initialize(guid);
        }

        public ViewIdentifierScript ViewIdentifierScript => _viewIdentifierScript;
    }
}