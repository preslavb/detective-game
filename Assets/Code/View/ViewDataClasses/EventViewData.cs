using Sirenix.OdinInspector;
using UnityEngine;
using View.Interfaces;
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
        private GameObject _eventPrefab;

        [SerializeField] 
        private Vector2? _startingPosition;

        public GameObject EventPrefab => _eventPrefab;
        public Vector2? StartingPosition => _startingPosition;
        public ViewIdentifierScript ViewIdentifierScript => _viewIdentifierScript;
    }
}