using System;

namespace Controller
{
    public static class GuidHandler
    {
        public static void SetUpGuids(BoardItemIdentifierLookupTable lookupTable)
        {
            foreach (var keyValuePair in lookupTable.LookUpTable)
            {
                var newGuid = Guid.NewGuid();

                keyValuePair.Value.ViewIdentifierScript.Guid = newGuid;
            }
        }
    }
}