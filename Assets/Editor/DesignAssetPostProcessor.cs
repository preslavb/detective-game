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
                // Custom Delete
                Debug.Log("Custom Delete Goes Here");
                return AssetDeleteResult.DidNotDelete;
            }

            return AssetDeleteResult.DidNotDelete;
        }
    }
}