using System.Collections.Generic;
using Model.BoardItemModels;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(order = 1, fileName = "PairResultLookUpTable", menuName = "Lookup Tables/Pair Result Look Up Table")]
    public class PairResultLookupTable: SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<BoardItemPair, BoardItemSerializable> _dictionary;

        public Dictionary<BoardItemPair, BoardItemSerializable> Dictionary => _dictionary;
        
        public BoardItemSerializable this[BoardItemPair pair] => _dictionary[pair];
    }
}