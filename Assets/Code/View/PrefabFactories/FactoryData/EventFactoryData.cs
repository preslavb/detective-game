using System;
using View.Scripts.Events;
using View.ViewDataClasses;

namespace View.PrefabFactories.FactoryData
{
    public struct EventFactoryData: IFactoryData
    {
        public EventViewData ViewData;
        public EventDetailsHandler EventDetailsHandler;
        public float ExpirationTime;
    }
}