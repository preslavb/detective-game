using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using View.PrefabFactories;
using View.PrefabFactories.FactoryData;
using View.Scripts;
using View.Scripts.MouseHandler;

namespace View
{
    public class ViewHandler
    {
        private ViewHandlerData _viewHandlerData;
        
        public ViewHandler(ViewHandlerData viewHandlerData)
        {
            _viewHandlerData = viewHandlerData;
            
            _factories = new Dictionary<Type, object>
            {
                { typeof(EvidenceFactory), new EvidenceFactory(_viewHandlerData) },
                { typeof(EventFactory), new EventFactory(_viewHandlerData) },
                { typeof(ResourceFactory), new ResourceFactory(_viewHandlerData) }
            };
        }
        

        public T GetFactory<T, Q>()
            where Q: IFactoryData
            where T: IPrefabFactory<Q>
        {
            return ((T) _factories[typeof(T)]);
        }
        
        public GameObject CreateWithFactory<T, TQ>(TQ dataToUse) 
            where TQ: IFactoryData
            where T: IPrefabFactory<TQ>
        {
            return ((T) _factories[typeof(T)]).CreateInstance(dataToUse);
        }
        
        public void Update(float timescale)
        {
            _viewHandlerData.TimeController.UpdateInput(timescale);
        }



        [NonSerialized] 
        private Dictionary<Type, object> _factories;
    }
}