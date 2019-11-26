using UnityEngine;
using View.Scripts;
using View.Scripts.Evidence;
using View.ViewDataClasses;

namespace View.PrefabFactories.FactoryData
{
    public struct EvidenceFactoryData: IFactoryData
    {
        public EvidenceViewData ViewData;
        public EvidenceDetailsHandler EvidenceDetailsHandlerReference;
    }
}