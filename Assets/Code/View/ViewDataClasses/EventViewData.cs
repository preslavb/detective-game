using Sirenix.OdinInspector;
using UnityEngine;
using View.Interfaces;

namespace View.ViewDataClasses
{
    public class EventViewData: IBoardItemViewProperties
    {
        [AssetsOnly]
        [SerializeField]
        private GameObject _boardPrefab;

        [AssetsOnly] 
        [SerializeField] 
        private GameObject _eventPrefab;

        [SerializeField] 
        private Vector2? _startingPosition;

        public GameObject BoardPrefab => _boardPrefab;
        public GameObject EventPrefab => _eventPrefab;
        public Vector2? StartingPosition => _startingPosition;
    }
}