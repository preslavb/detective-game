using Model;
using Model.BoardItemModels;
using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace View.ViewDataClasses
{
    public class EvidenceViewData : BoardItemViewProperties<Evidence>
    {
        [AssetsOnly]
        [SerializeField] 
        private GameObject _boardPrefab;
        
        [AssetsOnly]
        [SerializeField] 
        private GameObject _detailsPrefab;

        public GameObject BoardPrefab => _boardPrefab;
        public GameObject DetailsPrefab => _detailsPrefab;

        public override GameObject Instantiate(Transform root, IBoardItem boardItem)
        {
            return GameObject.Instantiate(_boardPrefab, root);
        }
    }
}