using UnityEngine;
using View.PrefabFactories.FactoryData;
using View.Scripts.Identifiers;

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
            // Instantiate the prefab
            var result = GameObject.Instantiate(factoryData.ViewData.ViewIdentifierScript.gameObject, ViewHandlerDataReference.FactoryRoot);

            result.GetComponent<ViewIdentifierScript>().Guid = factoryData.ViewData.ViewIdentifierScript.Guid;

            // Translate the object if a starting position was given
            result.transform.Translate(new Vector3(factoryData.ViewData.StartingPosition?.x ?? 0, factoryData.ViewData.StartingPosition?.y ?? 0), ViewHandlerDataReference.FactoryRoot);

            // Hook up the event data handler
            result.GetComponent<ClickHandlerScript>().OnPressRelease += () =>
                factoryData.EvidenceDetailsHandlerReference.TransitionToDetails(factoryData.ViewData.DetailsPrefab);

            // Initialize the board item position handler
            result.GetComponent<BoardItemPositionHandler>().Initialize(ViewHandlerDataReference.MainCamera, ViewHandlerDataReference.FactoryRoot);

            return result;
        }
    }
}