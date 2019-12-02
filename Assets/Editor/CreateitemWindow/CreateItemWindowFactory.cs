using DesignData;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Doozy.Editor.Nody.Windows
{
    public partial class NodyWindow
    {
        public partial class CreateItemWindow
        {
            private DesignDataScriptableObject CreateNewDesignDataObject()
            {
                var newDesignData = ScriptableObject.CreateInstance<DesignDataScriptableObject>();

                AssetDatabase.CreateAsset(newDesignData, "Assets/DesignData/New Design Data.asset");
                AssetDatabase.SaveAssets();

                return newDesignData;
            }
        }
    }
}