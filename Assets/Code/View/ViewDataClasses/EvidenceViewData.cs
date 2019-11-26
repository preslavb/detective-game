using Sirenix.OdinInspector;
using UnityEngine;
using View.Interfaces;

namespace View.ViewDataClasses
{
    public class EvidenceViewData : IBoardItemViewProperties
    {
        [AssetsOnly]
        [SerializeField] 
        private GameObject _boardPrefab;
        
        [AssetsOnly]
        [SerializeField] 
        private GameObject _detailsPrefab;

        [SerializeField] 
        private Vector2? _startingPosition;
        
        public GameObject BoardPrefab => _boardPrefab;
        public GameObject DetailsPrefab => _detailsPrefab;
        public Vector2? StartingPosition => _startingPosition;
    }
}