using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using View.Scripts.Identifiers;

namespace View.Scripts.Events
{
    [Serializable]
    public class SlotRequirementFilter
    {
        [HorizontalGroup("Split", 0.5f, LabelWidth = 20)]
        [BoxGroup("Split/Keys")]
        [ListDrawerSettings(Expanded = true)]
        [SerializeField] private List<SlotScript> _keys;
        
        [HorizontalGroup("Split", 0.5f, LabelWidth = 20)]
        [BoxGroup("Split/Values")]
        [ListDrawerSettings(Expanded = true)]
        [SerializeField] private List<ViewIdentifierScript> _values;

        public bool TryMatch(SlotScript[] inputs)
        {
            foreach (var input in inputs)
            {
                if (!_keys.Contains(input) || input.CurrentItemInSlotGuid != _values[_keys.IndexOf(input)].Guid) return false;
            }

            return true;
        }

        public bool CheckMatch(SlotScript slotScript, ViewIdentifierScript viewIdentifierScript)
        {
            if (!_keys.Contains(slotScript)) return true;

            if (_values[_keys.IndexOf(slotScript)].Guid == viewIdentifierScript.Guid) return true;

            return false;
        }
    }
}