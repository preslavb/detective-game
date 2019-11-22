using System;
using Controller;
using Model;
using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace View
{
    public class BoardItemSpawner: MonoBehaviour
    {
        public BoardItemPrefabLookupTable PrefabLookupTable => _prefabLookupTable;

        [SerializeField] [Required] private Transform _root;

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
    }
}