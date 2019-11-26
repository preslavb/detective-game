using UnityEngine;
using View.PrefabFactories.FactoryData;

namespace View.PrefabFactories
{
    public class EvidenceFactory: IPrefabFactory<EvidenceFactoryData>
    {
        public EvidenceFactory(ViewHandlerData viewHandlerDataReference)
        {
            ViewHandlerDataReference = viewHandlerDataReference;
        }

        public ViewHandlerData ViewHandlerDataReference { get; }

        public GameObject CreateInstance(EvidenceFactoryData factoryData)
        {
            throw new System.NotImplementedException();
        }
    }
}