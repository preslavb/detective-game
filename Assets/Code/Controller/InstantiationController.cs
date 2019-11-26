using System;
using System.Collections.Generic;
using Model.BoardItemModels;
using UnityEngine;
using View;
using View.PrefabFactories;
using View.PrefabFactories.FactoryData;
using View.Scripts.Events;
using View.ViewDataClasses;
using Event = Model.BoardItemModels.Event;

namespace Controller
{
    public class InstantiationController
    {
        private Dictionary<BoardItemSerializable, GameObject> _modelViewPairs;
        private ViewHandler _viewHandler;
        private ViewHandlerData _viewHandlerData;
        private BoardItemPrefabLookupTable _prefabLookupTable;
        
        public InstantiationController(
            Dictionary<BoardItemSerializable, GameObject> modelViewPairs,
            ViewHandler viewHandler,
            ViewHandlerData viewHandlerData,
            BoardItemPrefabLookupTable prefabLookupTable
        ){
            _modelViewPairs = modelViewPairs;
            _viewHandler = viewHandler;
            _viewHandlerData = viewHandlerData;
            _prefabLookupTable = prefabLookupTable;
        }
        
        public void InstantiateItem(BoardItemSerializable item)
        {
            GameObject result = null;
                
            switch (item)
            {
                case Event @event:
                    InstantiateEvent(@event, out result);
                    break;
                case Evidence evidence:
                    InstantiateEvidence(evidence, out result);
                    break;
                case Resource resource:
                    InstantiateResource(resource, out result);
                    break;
                default:
                    throw new Exception("The switch statement does not cover all possible Board item types");
            }
                
            _modelViewPairs.Add(item, result);
        }

        private void InstantiateEvent(Event @event, out GameObject result)
        {
            result = _viewHandler.CreateWithFactory<EventFactory, EventFactoryData>(new EventFactoryData
            {
                ViewData = (EventViewData) _prefabLookupTable[@event],
                ExpirationTime = @event.ExpirationTime
            });
                    
            // Hook up dependencies
            var eventScript = result.GetComponent<EventScript>();

            @event.OnTimerChange += eventScript.UpdateExpirable;
        }

        private void InstantiateEvidence(Evidence evidence, out GameObject result)
        {
            result = _viewHandler.CreateWithFactory<EvidenceFactory, EvidenceFactoryData>(new EvidenceFactoryData
            {
                ViewData = (EvidenceViewData) _prefabLookupTable[evidence],
                EvidenceDetailsHandlerReference = _viewHandlerData.EvidenceDetailsHandler
            });
        }

        private void InstantiateResource(Resource resource, out GameObject result)
        {
            result = _viewHandler.CreateWithFactory<ResourceFactory, ResourceFactoryData>(new ResourceFactoryData
            {
                ViewData = (ResourceViewData) _prefabLookupTable[resource]
            });
        }
    }
}