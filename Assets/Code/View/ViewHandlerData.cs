using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using View.Scripts;
using View.Scripts.Events;
using View.Scripts.Evidence;
using View.Scripts.MouseHandler;

namespace View
{
    [Serializable]
    public class ViewHandlerData
    {
        // Serialized props
        [FormerlySerializedAs("_evidenceDetailsHandler")] [SerializeField] [Required] private DetailsHandler _detailsHandler;
        [SerializeField] [Required] private EventDetailsHandler _eventDetailsHandler;
        [SerializeField] [Required] private MouseHandler _mouseHandler;
        [SerializeField] [Required] private TimeControllerScript _timeController;
        [SerializeField] [Required] private Transform _factoryRoot;
        [SerializeField] [Required] private Camera _mainCamera;
        
        // Public getters
        public DetailsHandler DetailsHandler => _detailsHandler;
        public EventDetailsHandler EventDetailsHandler => _eventDetailsHandler;
        public MouseHandler MouseHandler => _mouseHandler;
        public TimeControllerScript TimeController => _timeController;
        public Transform FactoryRoot => _factoryRoot;
        public Camera MainCamera => _mainCamera;
    }
}