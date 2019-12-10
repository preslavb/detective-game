using View.Scripts.Evidence;
using View.ViewDataClasses;

namespace View.PrefabFactories.FactoryData
{
    public class ResourceFactoryData: IFactoryData
    {
        public ResourceViewData ViewData { get; set; }
        public DetailsHandler DetailsHandlerReference { get; set; }
    }
}