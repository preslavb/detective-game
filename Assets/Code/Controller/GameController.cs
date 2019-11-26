using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.BoardItemModels;
using Sirenix.OdinInspector;
using UnityEngine;
using View;
using View.Scripts;
using View.Scripts.Identifiers;

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
        private BoardItemIdentifierLookupTable _identifierLookupTable;
        
        [ReadOnly] [ShowInInspector]
        private Dictionary<BoardItemSerializable, Guid> _modelViewGuids;

        [ReadOnly] [ShowInInspector]
        private Dictionary<Guid, GameObject> _guidView;
        
        public void Start()
        {
            _modelViewGuids = new Dictionary<BoardItemSerializable, Guid>();
            _guidView = new Dictionary<Guid, GameObject>();
            
            GuidHandler.SetUpGuids(_identifierLookupTable);
            
            // Create the model
            _modelSimulation = new ModelSimulation(_modelSimulationData);
            
            // Create the view handler
            _viewHandler = new ViewHandler(_viewHandlerData);
            
            // Set up the time controller
            _viewHandlerData.TimeController.ChangedTimescale +=
                timescale => _modelSimulation.GameTime.TimeScale = timescale;
            
            _instantiationController = new InstantiationController(
                _modelViewGuids,
                _viewHandler,
                _viewHandlerData,
                _identifierLookupTable
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

        private BoardItemPair ConstructPair(ViewIdentifierScript[] scripts)
        {
            var firstGuid = scripts[0].Guid;
            var secondGuid = scripts[1].Guid;

            var firstKey = _modelViewGuids.FirstOrDefault(x => x.Value == firstGuid).Key;
            var secondKey = _modelViewGuids.FirstOrDefault(x => x.Value == secondGuid).Key;

            return new BoardItemPair(firstKey, secondKey);
        }

        private InstantiationController _instantiationController;
    }
}