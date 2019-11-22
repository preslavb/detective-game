using System.Collections.Generic;
using Model;
using Model.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using View;
using View.Interfaces;

namespace Controller
{
    [CreateAssetMenu(order = 2, fileName = "BoardItemLookUpTable", menuName = "Board Item Data/Prefab Look Up Table")]
    public class BoardItemPrefabLookupTable: SerializedScriptableObject
    {
        public Dictionary<IBoardItem, IBoardItemViewProperties> LookUpTable => _lookUpTable;

        [OdinSerialize]
        private Dictionary<IBoardItem, IBoardItemViewProperties> _lookUpTable;
    }
}