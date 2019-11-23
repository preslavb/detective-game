using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = Model.BoardItemModels.Event;

public class EventDetailsScript : MonoBehaviour
{
    [SerializeField]
    private Event _event;

    public Event Event => _event;
}
