using System;
using System.Collections.Generic;
using Model.BoardItemModels;
using UnityEngine;
using View;
using View.PrefabFactories;
using View.PrefabFactories.FactoryData;
using View.Scripts.Events;
using View.Scripts.Identifiers;
using View.ViewDataClasses;
using Event = Model.BoardItemModels.Event;

namespace Controller
{
    public class InstantiationController
    {
        private Dictionary<BoardItemSerializable, Guid> _modelViewPairs;
        private Dictionary<Guid, ViewIdentifierScript> _guidView;
        private ViewHandler _viewHandler;
        private ViewHandlerData _viewHandlerData;
        private BoardItemIdentifierLookupTable _identifierLookupTable;
        
        public InstantiationController(
            Dictionary<BoardItemSerializable, Guid> modelViewPairs,
            Dictionary<Guid, ViewIdentifierScript> guidView,
            ViewHandler viewHandler,
            ViewHandlerData viewHandlerData,
            BoardItemIdentifierLookupTable identifierLookupTable
        ){
            _modelViewPairs = modelViewPairs;
            _guidView = guidView;
            _viewHandler = viewHandler;
            _viewHandlerData = viewHandlerData;
            _identifierLookupTable = identifierLookupTable;
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

            var identifierScript = result.GetComponent<ViewIdentifierScript>();
            
            if (!_modelViewPairs.ContainsKey(item))
                _modelViewPairs.Add(item, identifierScript.Guid);

            if (!_guidView.ContainsKey(identifierScript.Guid))
                _guidView.Add(identifierScript.Guid, identifierScript);
        }

        private void InstantiateEvent(Event @event, out GameObject result)
        {
            result = _viewHandler.CreateWithFactory<EventFactory, EventFactoryData>(new EventFactoryData
            {
                ViewData = (EventViewData) _identifierLookupTable[@event],
                EventDetailsHandler = _viewHandlerData.EventDetailsHandler,
                ExpirationTime = @event.ExpirationTime
            });
                    
            // Hook up dependencies
            var eventScript = result.GetComponent<EventCooldownScript>();

            @event.OnTimerChange += eventScript.UpdateExpirable;
        }

        private void InstantiateEvidence(Evidence evidence, out GameObject result)
        {
            result = _viewHandler.CreateWithFactory<EvidenceFactory, EvidenceFactoryData>(new EvidenceFactoryData
            {
                ViewData = (EvidenceViewData) _identifierLookupTable[evidence],
                EvidenceDetailsHandlerReference = _viewHandlerData.EvidenceDetailsHandler
            });
        }

        private void InstantiateResource(Resource resource, out GameObject result)
        {
            result = _viewHandler.CreateWithFactory<ResourceFactory, ResourceFactoryData>(new ResourceFactoryData
            {
                ViewData = (ResourceViewData) _identifierLookupTable[resource]
            });
        }
    }
}