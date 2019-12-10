using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View.Scripts.Evidence
{
    public class DetailsHandler: MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _boardCamera;
        [SerializeField] private CinemachineVirtualCamera _detailsCamera;

        [SerializeField] private Button _backButton;

        [SerializeField] 
        [SceneObjectsOnly]
        private Transform _detailsRoot;

        private GameObject _spawnedGameObject;

        private void Start()
        {
            _backButton.onClick.AddListener(TransitionBack);
        }

        public void TransitionToDetails(GameObject prefabToUse)
        {
            // Switch to the details camera
            _boardCamera.gameObject.SetActive(false);
            
            // Spawn the prefab at the correct place
            _spawnedGameObject = Instantiate(prefabToUse, _detailsRoot);
        }

        private void TransitionBack()
        {
            _boardCamera.gameObject.SetActive(true);
            
            Destroy(_spawnedGameObject);
        }
    }
}