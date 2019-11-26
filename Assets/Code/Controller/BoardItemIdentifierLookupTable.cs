using System.Collections.Generic;
using Model.BoardItemModels;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using View.Interfaces;

namespace Controller
{
    [CreateAssetMenu(order = 0, fileName = "BoardItemLookUpTable", menuName = "Lookup Tables/Prefab Look Up Table")]
    public class BoardItemIdentifierLookupTable: SerializedScriptableObject
    {
        public Dictionary<BoardItemSerializable, IBoardItemViewProperties> LookUpTable => _lookUpTable;

        [OdinSerialize]
        private Dictionary<BoardItemSerializable, IBoardItemViewProperties> _lookUpTable;
        
        public IBoardItemViewProperties this[BoardItemSerializable key] => _lookUpTable[key];
    }
}