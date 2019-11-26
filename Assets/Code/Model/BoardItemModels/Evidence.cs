using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using View;

namespace Model.BoardItemModels
{
    [CreateAssetMenu(order = 0, fileName = "Evidence 1", menuName = "Board Item Data/Evidence")]
    public class Evidence: BoardItemSerializable
    {
        public override void Update()
        {
        }
        
        #if UNITY_EDITOR

        [OdinSerialize][ReadOnly]
        [InfoBox("$_errorMessage", InfoMessageType.Error, "HasNoPrefab")]
        private string _errorMessage;

        private bool HasNoPrefab()
        {
            var evidenceSpawner = GameObject.FindObjectOfType<BoardItemSpawner>();

            if (evidenceSpawner == null)
            {
                _errorMessage = "No active lookup table found.";
                return true;
            }

            if (!evidenceSpawner.PrefabLookupTable.LookUpTable.ContainsKey(this))
            {
                _errorMessage = "Evidence is not registered in the currently active lookup table.";
                return true;
            }
            
            else if (evidenceSpawner.PrefabLookupTable.LookUpTable[this] == null)
            {
                _errorMessage = "Evidence does not have a registered prefab in the currently active lookup table.";
                return true;
            }

            _errorMessage = "";
            return false;
        }
        
        #endif
    }
}