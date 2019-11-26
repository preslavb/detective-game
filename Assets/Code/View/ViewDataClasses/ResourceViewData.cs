using Sirenix.OdinInspector;
using UnityEngine;
using View.Interfaces;

namespace View.ViewDataClasses
{
    public class ResourceViewData: IBoardItemViewProperties
    {
        [AssetsOnly] 
        [SerializeField] 
        private GameObject _boardPrefab;
        
        [SerializeField] 
        private Vector2? _startingPosition;

        public GameObject BoardPrefab => _boardPrefab;
        public Vector2? StartingPosition => _startingPosition;
    }
}