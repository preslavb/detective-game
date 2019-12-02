using DesignData;

namespace Doozy.Editor.Nody.Windows
{
    public partial class NodyWindow
    {
        public partial class CreateItemWindow
        {
            public void LoadDesignData(DesignDataScriptableObject designData)
            {
                _designData = designData;
            }
        }
    }
}