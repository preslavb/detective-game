using System;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace View.Scripts
{
    public class EvidenceDetailsHandler: MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _boardCamera;
        [SerializeField] private CinemachineVirtualCamera _detailsCamera;

        [SerializeField] 
        [SceneObjectsOnly]
        private Transform _detailsRoot;

        private void Start()
        {
            _boardCamera.MoveToTopOfPrioritySubqueue();
        }

        public void TransitionToDetails(GameObject prefabToUse)
        {
            // Switch to the details camera
            _detailsCamera.MoveToTopOfPrioritySubqueue();
            
            // Spawn the prefab at the correct place
            Instantiate(prefabToUse, _detailsRoot);
        }
    }
}