using Doozy.Engine.Nody.Models;
using UnityEditor;
using UnityEngine;
using View.Scripts.Events;
using View.Scripts.Identifiers;
using View.ViewDataClasses;

namespace DesignData
{
    public partial class DesignDataScriptableObject
    {
        public void OnCreate()
        {
            _dataType = DesignDataType.Event;
            
            Object originalPrefabDetails = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Event Screens/Event Screen Prefab.prefab", typeof(GameObject));
            Object originalPrefabBoard = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Board Representations/Events/Event.prefab", typeof(GameObject));
            
            GameObject detailsSource = PrefabUtility.InstantiatePrefab(originalPrefabDetails) as GameObject;
            GameObject boardSource = PrefabUtility.InstantiatePrefab(originalPrefabBoard) as GameObject;

            AssetDatabase.CreateFolder("Assets\\Prefabs\\Event Screens", Name);
            
            GameObject prefabVariantDetails = PrefabUtility.SaveAsPrefabAsset(detailsSource, $"Assets/Prefabs/Event Screens/{Name}/{Name} Details.prefab");
            GameObject prefabVariantBoard = PrefabUtility.SaveAsPrefabAsset(boardSource, $"Assets/Prefabs/Board Representations/Events/{Name} Prefab.prefab");

            Graph newGraph = ScriptableObject.CreateInstance<Graph>();
            newGraph.name = $"{Name} Nody Graph";
            
            AssetDatabase.CreateAsset(newGraph, $"Assets/Prefabs/Event Screens/{Name}/{newGraph.name}.asset");

            EventGraph = newGraph;

            ViewProperties = new EventViewData(
                prefabVariantBoard.GetComponent<ViewIdentifierScript>(), 
                prefabVariantDetails.GetComponent<EventDetailsScript>(), 
                null);
            
            DestroyImmediate(detailsSource);
            DestroyImmediate(boardSource);
        }
    }
}