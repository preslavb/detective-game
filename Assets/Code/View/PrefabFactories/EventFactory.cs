using UnityEngine;
using View.PrefabFactories.FactoryData;
using View.Scripts.Events;
using View.Scripts.Identifiers;

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
            var result = GameObject.Instantiate(factoryData.ViewData.ViewIdentifierScript.gameObject, ViewHandlerDataReference.FactoryRoot);

            result.GetComponent<ViewIdentifierScript>().Guid = factoryData.ViewData.ViewIdentifierScript.Guid;

            // Translate the object if a starting position was given
            result.transform.Translate(new Vector3(factoryData.ViewData.StartingPosition?.x ?? 0, factoryData.ViewData.StartingPosition?.y ?? 0), ViewHandlerDataReference.FactoryRoot);

            // Set up the radial countdown 
            if (factoryData.ExpirationTime > 0)
            {
                result.GetComponent<EventCooldownScript>().RadialCountdown.gameObject.SetActive(true);
                result.GetComponent<EventCooldownScript>().RadialCountdown.fillAmount = 1;
            }

            else
            {
                result.GetComponent<EventCooldownScript>().RadialCountdown.gameObject.SetActive(false);
            }
            
            // Hook up the event data handler
            result.GetComponent<ClickHandlerScript>().OnPressRelease += () =>
                factoryData.EventDetailsHandler.ShowDetailsForEvent(factoryData.ViewData.EventDetailsPrefab);
            
            // Initialize the board item position handler
            result.GetComponent<BoardItemPositionHandler>().Initialize(ViewHandlerDataReference.MainCamera, ViewHandlerDataReference.FactoryRoot);

            return result;
        }
    }
}