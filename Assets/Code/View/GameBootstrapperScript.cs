using System;
using Model;
using UnityEngine;

namespace View
{
    public class GameBootstrapperScript: MonoBehaviour
    {
        private static GameBootstrapperScript _instance;

        public static GameBootstrapperScript Instance => _instance;

        private ModelSimulation _modelSimulation;

        [SerializeField]
        private BoardItemSerializable[] _initialBoardItems;

        private void Awake()
        {
            _instance = this;
            
            _modelSimulation = new ModelSimulation(_initialBoardItems);
        }
    }
}