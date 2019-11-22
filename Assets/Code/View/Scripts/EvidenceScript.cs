using Model;
using Model.BoardItemModels;
using Model.Interfaces;
using UnityEngine;
using View.ViewDataClasses;

namespace View.Scripts
{
    [RequireComponent(typeof(ClickHandlerScript))]
    public class EvidenceScript: BoardItemScript
    {
        [SerializeField]
        private Evidence _evidence;

        private ClickHandlerScript _clickHandlerScript;

        private BoardItemSpawner _boardItemSpawner;
        
        private EvidenceDetailsHandler _detailsHandler;

        private void Awake()
        {
            _clickHandlerScript = GetComponent<ClickHandlerScript>();

            _detailsHandler = GetComponentInParent<EvidenceDetailsHandler>();
            _boardItemSpawner = GetComponentInParent<BoardItemSpawner>();

            _clickHandlerScript.OnPressRelease += ShowDetails;
        }

        private void ShowDetails()
        {
            // Cast to appropriate data
            var detailsPrefab = ((EvidenceViewData) _boardItemSpawner.PrefabLookupTable.LookUpTable[_evidence]).DetailsPrefab;

            _detailsHandler.TransitionToDetails(detailsPrefab);
        }

        public override BoardItemSerializable BoardItem => _evidence;
    }
}