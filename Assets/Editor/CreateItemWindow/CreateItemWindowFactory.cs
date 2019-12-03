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
            [SerializeField]
            private string _newDesignDataName = "New Design Data";
            
            private DesignDataScriptableObject CreateNewDesignDataObject()
            {
                var newDesignData = ScriptableObject.CreateInstance<DesignDataScriptableObject>();
                newDesignData.name = _newDesignDataName;
                
                newDesignData.OnCreate();

                AssetDatabase.CreateAsset(newDesignData, $"Assets/DesignData/{_newDesignDataName}.asset");
                AssetDatabase.SaveAssets();

                return newDesignData;
            }
        }
    }
}