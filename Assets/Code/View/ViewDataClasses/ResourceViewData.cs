using Model.BoardItemModels;
using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace View.ViewDataClasses
{
    public class ResourceViewData: BoardItemViewProperties
    {
        [AssetsOnly] 
        [SerializeField] 
        private GameObject _boardPrefab;
        
        public override GameObject Instantiate(Transform root, IBoardItem boardItem)
        {
            return GameObject.Instantiate(_boardPrefab, root);
        }
    }
}