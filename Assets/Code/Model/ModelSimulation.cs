using System.Collections.Generic;
using Model.BoardItemModels;
using Sirenix.OdinInspector;

namespace Model
{
    public class ModelSimulation
    {
        private GameTime _gameTime;
        private Board _board;
        private PairResolver _pairResolver;

        public ModelSimulationData ModelSimulationDataReference { get; }

        public GameTime GameTime => _gameTime;
        public Board Board => _board;
        public PairResolver PairResolver => _pairResolver;

        public ModelSimulation(ModelSimulationData modelSimulationData)
        {
            ModelSimulationDataReference = modelSimulationData;
            _gameTime = new GameTime();
            _board = new Board();
            _pairResolver = new PairResolver(this);

            foreach (var boardItemSerializable in ModelSimulationDataReference.StartingItems)
            {
                boardItemSerializable.Initialize(_gameTime);
                _board.InsertItem(boardItemSerializable);
            }
        }

        public void InsertIntoBoard(BoardItemSerializable item)
        {
            item.Initialize(_gameTime);
            _board.InsertItem(item);
        }

        public void Update(float deltaTime)
        {
            _gameTime.Tick(deltaTime);
        }
    }
}