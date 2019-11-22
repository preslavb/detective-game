using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Event = Model.BoardItemModels.Event;

namespace View.ViewDataClasses
{
    public class EventViewData: BoardItemViewProperties<Event>
    {
        [AssetsOnly]
        [SerializeField]
        private GameObject _boardPrefab;

        [AssetsOnly] 
        [SerializeField] 
        private GameObject _eventPrefab;
        
        public override GameObject Instantiate(Transform root, IBoardItem boardItem)
        {
            // Cast to the type
            var boardItemTypeCast = (Event)boardItem;
            
            // Instantiate the prefab
            return GameObject.Instantiate(_boardPrefab, root);
        }
    }
}