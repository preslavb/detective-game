using System;
using DesignData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DesignAssetPostProcessor: UnityEditor.AssetModificationProcessor
    {
        private static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
        {
            var designAsset = AssetDatabase.LoadAssetAtPath<DesignDataScriptableObject>(assetPath);

            if (designAsset != null)
            {
                CleanDesignAssetData(designAsset);
                
                return AssetDeleteResult.DidNotDelete;
            }

            return AssetDeleteResult.DidNotDelete;
        }

        public static void CleanDesignAssetData(DesignDataScriptableObject designData) 
        {
            if (designData.DataType == DesignDataScriptableObject.DesignDataType.Event)
            {
                // Find the event screen folder
                FileUtil.DeleteFileOrDirectory($"Assets/Prefabs/Event Screens/{designData.name}");
                FileUtil.DeleteFileOrDirectory($"Assets/Prefabs/Board Representations/Events/{designData.name} Prefab.prefab");
            }
        }
    }
}