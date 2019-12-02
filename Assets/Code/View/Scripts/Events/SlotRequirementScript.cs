using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;
using View.Scripts.Identifiers;

namespace View.Scripts.Events
{
    public enum EventPageEndpointRequirement
    {
        None,
        AllMustBeEmpty,
        AllMustBeFull,
        MustFitFilter
    }
    
    [RequireComponent(typeof(Button))]
    public class SlotRequirementScript: SerializedMonoBehaviour
    {
        [SerializeField] private EventPageEndpointRequirement _requirement;
        
        [SerializeField] private SlotScript[] slotsToWaitFor;

        [SerializeField]
        [Required]
        [ShowIf("IsUsingFilter")]
        private SlotRequirementFilter _requirementFilter;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.interactable = false;

            foreach (var slotScript in slotsToWaitFor)
            {
                slotScript.GetExternalRequirements += ReturnExternalRequirementResult;
                slotScript.DidChangeState += Reevaluate;
            }
        }

        private bool ReturnExternalRequirementResult(SlotScript scriptToCheck, ViewIdentifierScript viewIdentifierScript)
        {
            if (_requirement != EventPageEndpointRequirement.MustFitFilter) return true;

            return _requirementFilter.CheckMatch(scriptToCheck, viewIdentifierScript);
        }

        private void Reevaluate()
        {
            _button.interactable = true;

            foreach (var slotScript in slotsToWaitFor)
            {
                switch (_requirement)
                {
                    case EventPageEndpointRequirement.None:
                        break;
                    case EventPageEndpointRequirement.AllMustBeEmpty:
                        if (slotScript.CurrentItemInSlotGuid != Guid.Empty)
                        {
                            _button.interactable = false;
                            return;
                        }

                        break;
                    case EventPageEndpointRequirement.AllMustBeFull:
                        if (slotScript.CurrentItemInSlotGuid == Guid.Empty)
                        {
                            _button.interactable = false;
                            return;
                        }
                        
                        break;
                    case EventPageEndpointRequirement.MustFitFilter:
                        if (!_requirementFilter.TryMatch(slotsToWaitFor))
                        {
                            _button.interactable = false;
                        }
                        break;
                }
            }
        }

#if UNITY_EDITOR
        private bool IsUsingFilter => _requirement == EventPageEndpointRequirement.MustFitFilter;
#endif
    }
}