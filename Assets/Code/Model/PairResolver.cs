using System.Collections.Generic;
using Model.BoardItemModels;

namespace Model
{
    public class PairResolver
    {
        private ModelSimulation _modelSimulationReference;

        public PairResolver(ModelSimulation modelSimulationReference)
        {
            _modelSimulationReference = modelSimulationReference;
        }

        public bool Resolve(BoardItemPair boardItemPair)
        {
            _modelSimulationReference.ModelSimulationDataReference.PairResults.Dictionary.TryGetValue(boardItemPair, out var result);

            if ((result?.Length ?? 0) == 0) return false;
            
            // Add the items to the model
            foreach (var boardItemSerializable in result)
            {
                _modelSimulationReference.InsertIntoBoard(boardItemSerializable);
            }

            return true;
        }
    }
}