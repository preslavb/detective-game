using Model;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace View.Scripts
{
    public class EventDecisionScript: SerializedMonoBehaviour
    {
        [OdinSerialize] private EventDecision _eventDecision;
    }
}