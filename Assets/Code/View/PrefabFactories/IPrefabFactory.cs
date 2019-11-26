using UnityEngine;
using View.PrefabFactories.FactoryData;

namespace View.PrefabFactories
{
    public interface IPrefabFactory<in T> where T: IFactoryData
    {
        ViewHandlerData ViewHandlerDataReference { get; }
        GameObject CreateInstance(T factoryData);
    }
}