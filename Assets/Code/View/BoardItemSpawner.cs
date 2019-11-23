using System;
using Controller;
using Model;
using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using View.ViewDataClasses;
using Event = Model.BoardItemModels.Event;

namespace View
{
    public class BoardItemSpawner: MonoBehaviour
    {
        public BoardItemPrefabLookupTable PrefabLookupTable => _prefabLookupTable;

        [SerializeField] [Required] private Transform _root;

        [SerializeField] [Required] private Transform _eventDetailsRoot;

        [SerializeField] [Required]
        private BoardItemPrefabLookupTable _prefabLookupTable;

        [Button]
        public GameObject Spawn(BoardItemSerializable boardItem)
        {
            // Check if there is a prefab for this board item
            if (_prefabLookupTable.LookUpTable.ContainsKey(boardItem))
            {
                GameObject gameObject = _prefabLookupTable.LookUpTable[boardItem].Instantiate(_root, boardItem);
                gameObject.name = boardItem.Name;

                return gameObject;
            }

            else
            {
                throw new Exception ($"Board item {boardItem.Name} does not have a registered prefab. Please add one to the \"{_prefabLookupTable.name}\" Lookup Table");
            }
        }

        public void OpenEventDetails(Event eventToOpen)
        {
            EventViewData eventViewData = (EventViewData)_prefabLookupTable.LookUpTable[eventToOpen];

            var gameObjectCopy = Instantiate(eventViewData.EventPrefab, _eventDetailsRoot);
            var rectTrans = _eventDetailsRoot.GetComponent<RectTransform>();

            gameObjectCopy.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, rectTrans.rect.width);
            gameObjectCopy.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rectTrans.rect.height);
        }
    }
}