using System;
using System.Collections.Generic;
using Model.BoardItemModels;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class ModelSimulationData
    {
        [SerializeField] private PairResultLookupTable _pairResults;
        [SerializeField] private BoardItemSerializable[] _startingItems;

        public PairResultLookupTable PairResults => _pairResults;
        public BoardItemSerializable[] StartingItems => _startingItems;
    }
}