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

            if (result == null) return false;
            
            // Add the item to the model
            _modelSimulationReference.InsertIntoBoard(result);

            return true;
        }
    }
}