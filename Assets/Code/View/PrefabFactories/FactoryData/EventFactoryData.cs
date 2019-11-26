using System;
using View.ViewDataClasses;

namespace View.PrefabFactories.FactoryData
{
    public struct EventFactoryData: IFactoryData
    {
        public EventViewData ViewData;
        public float ExpirationTime;
    }
}