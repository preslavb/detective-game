using System;
using UnityEngine;
using UnityEngine.UI;

namespace View.Scripts.Events
{
    public enum EventPageEndpointRequirement
    {
        None,
        AllMustBeEmpty,
        AllMustBeFull
    }
    
    [RequireComponent(typeof(Button))]
    public class EventPageEndpointScript: MonoBehaviour
    {
        [SerializeField] private EventPageEndpointRequirement _requirement;
        
        [SerializeField] private SlotScript[] slotsToWaitFor;

        private Button _buttonToCloseEvent;

        private void Awake()
        {
            _buttonToCloseEvent = GetComponent<Button>();
        }

        private void Start()
        {
            _buttonToCloseEvent.interactable = false;

            foreach (var slotScript in slotsToWaitFor)
            {
                slotScript.DidChangeState += Reevaluate;
            }
        }

        private void Reevaluate()
        {
            _buttonToCloseEvent.interactable = true;

            foreach (var slotScript in slotsToWaitFor)
            {
                switch (_requirement)
                {
                    case EventPageEndpointRequirement.None:
                        break;
                    case EventPageEndpointRequirement.AllMustBeEmpty:
                        if (slotScript.IdentifierPrefab != null)
                        {
                            _buttonToCloseEvent.interactable = false;
                            return;
                        }

                        break;
                    case EventPageEndpointRequirement.AllMustBeFull:
                        if (slotScript.IdentifierPrefab == null)
                        {
                            _buttonToCloseEvent.interactable = false;
                            return;
                        }
                        
                        break;
                }
            }
        }
    }
}