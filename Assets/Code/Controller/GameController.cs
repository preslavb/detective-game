using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.BoardItemModels;
using Sirenix.OdinInspector;
using UnityEngine;
using View;
using View.Scripts;

namespace Controller
{
    [Serializable]
    public class GameController
    {

        [TabGroup("Model")] [ReadOnly] [ShowInInspector]
        private ModelSimulation _modelSimulation;
        
        [TabGroup("Model")] [SerializeField]
        private ModelSimulationData _modelSimulationData;


        [TabGroup("View")] [ReadOnly] [ShowInInspector]
        private ViewHandler _viewHandler;

        [TabGroup("View")] [SerializeField] [Required]
        private ViewHandlerData _viewHandlerData;

        [SerializeField]
        private BoardItemPrefabLookupTable _prefabLookupTable;
        
        [ReadOnly] [ShowInInspector]
        private Dictionary<BoardItemSerializable, GameObject> _modelViewPairs;
        
        public void Start()
        {
            _modelViewPairs = new Dictionary<BoardItemSerializable, GameObject>();
            
            // Create the model
            _modelSimulation = new ModelSimulation(_modelSimulationData);
            
            // Create the view handler
            _viewHandler = new ViewHandler(_viewHandlerData);
            
            // Set up the time controller
            _viewHandlerData.TimeController.ChangedTimescale +=
                timescale => _modelSimulation.GameTime.TimeScale = timescale;
            
            _instantiationController = new InstantiationController(
                _modelViewPairs,
                _viewHandler,
                _viewHandlerData,
                _prefabLookupTable
            );

            // Instantiate the starting item views
            foreach (var startingItem in _modelSimulationData.StartingItems)
            {
                _instantiationController.InstantiateItem(startingItem);
            }
            
            // Subscribe to the model actions
            _modelSimulation.Board.DidInsertItem += _instantiationController.InstantiateItem;
            
            // Subscribe the game view actions
            _viewHandlerData.MouseHandler.DeductionModeClickHandler.OnCreatedAPair += scripts => _modelSimulation.PairResolver.Resolve(ConstructPair(scripts));
        }

        public void Update()
        {
            // Update the model
            _modelSimulation.Update(Time.deltaTime);
            
            // Update the view
            _viewHandler.Update(_modelSimulation.GameTime.TimeScale);
        }

        private BoardItemPair ConstructPair(BoardItemScript[] scripts)
        {
            var firstGO = scripts[0].gameObject;
            var secondGO = scripts[1].gameObject;

            var firstKey = _modelViewPairs.FirstOrDefault(x => x.Value == firstGO).Key;
            var secondKey = _modelViewPairs.FirstOrDefault(x => x.Value == secondGO).Key;

            return new BoardItemPair(firstKey, secondKey);
        }

        private InstantiationController _instantiationController;
    }
}