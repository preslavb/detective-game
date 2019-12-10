using System;
using System.Collections.Generic;
using UnityEngine;
using View.PrefabFactories;
using View.PrefabFactories.FactoryData;
using View.Scripts.Identifiers;

namespace View
{
    [CreateAssetMenu(order = 3, fileName = "View Handler", menuName = "View Handler")]
    public class ViewHandler: ScriptableObject
    {
        public delegate ViewIdentifierScript ViewInsertItemDelegate(Guid guid);
        
        private ViewHandlerData _viewHandlerData;

        public event ViewInsertItemDelegate OnItemInsertRequest;

        public ViewHandlerData ViewHandlerData => _viewHandlerData;
        
        public void InitializeViewHandler(ViewHandlerData viewHandlerData)
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
        
        public GameObject CreateWithFactory<T, TQ>(TQ dataToUse, string name) 
            where TQ: IFactoryData
            where T: IPrefabFactory<TQ>
        {
            return ((T) _factories[typeof(T)]).CreateInstance(dataToUse, name);
        }
        
        public void UpdateViewHandler(float timescale)
        {
            _viewHandlerData.TimeController.UpdateInput(timescale);
        }

        public ViewIdentifierScript InsertItemByGuid(Guid guid)
        {
            return OnItemInsertRequest?.Invoke(guid);
        }

        [NonSerialized] 
        private Dictionary<Type, object> _factories;
    }
}