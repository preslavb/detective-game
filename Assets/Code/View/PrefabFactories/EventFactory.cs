using UnityEngine;
using View.PrefabFactories.FactoryData;
using View.Scripts.Events;

namespace View.PrefabFactories
{
    public class EventFactory: IPrefabFactory<EventFactoryData>
    {
        public EventFactory(ViewHandlerData viewHandlerDataReference)
        {
            ViewHandlerDataReference = viewHandlerDataReference;
        }

        public ViewHandlerData ViewHandlerDataReference { get; }

        public GameObject CreateInstance(EventFactoryData factoryData)
        {
            // Instantiate the prefab
            var result = GameObject.Instantiate(factoryData.ViewData.BoardPrefab, ViewHandlerDataReference.FactoryRoot);

            // Translate the object if a starting position was given
            result.transform.Translate(new Vector3(factoryData.ViewData.StartingPosition?.x ?? 0, factoryData.ViewData.StartingPosition?.y ?? 0), ViewHandlerDataReference.FactoryRoot);

            // Set up the radial countdown 
            if (factoryData.ExpirationTime > 0)
            {
                result.GetComponent<EventScript>().RadialCountdown.gameObject.SetActive(true);
                result.GetComponent<EventScript>().RadialCountdown.fillAmount = 1;
            }

            else
            {
                result.GetComponent<EventScript>().RadialCountdown.gameObject.SetActive(false);
            }
            
            // Initialize the board item position handler
            result.GetComponent<BoardItemPositionHandler>().Initialize(ViewHandlerDataReference.MainCamera, ViewHandlerDataReference.FactoryRoot);

            return result;
        }
    }
}