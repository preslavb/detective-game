using System;
using System.Collections.Generic;
using Model.BoardItemModels;

namespace Controller
{
    public static class GuidHandler
    {
        public static void SetUpGuids(BoardItemIdentifierLookupTable lookupTable, Dictionary<BoardItemSerializable, Guid> boardItemGuids)
        {
            foreach (var keyValuePair in lookupTable.LookUpTable)
            {
                var newGuid = Guid.NewGuid();

                keyValuePair.Value.Initialize(newGuid);

                if (!boardItemGuids.ContainsKey(keyValuePair.Key))
                    boardItemGuids.Add(keyValuePair.Key, newGuid);
            }
        }
    }
}