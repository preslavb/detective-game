using System.Collections.Generic;
using System.Linq;
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

            _modelSimulationReference.ModelSimulationDataReference.PairResults.Dictionary.FirstOrDefault(pair =>
                pair.Key.Equals(boardItemPair)).Key.HasBeenInstantiated = true;
            
            // Add the items to the model
            foreach (var boardItemSerializable in result)
            {
                _modelSimulationReference.InsertIntoBoard(boardItemSerializable);
            }

            return true;
        }
    }
}