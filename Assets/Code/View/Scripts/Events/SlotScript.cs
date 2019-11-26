using UnityEngine;

namespace View.Scripts.Events
{
    [RequireComponent(typeof(ClickHandlerScript))]
    public class SlotScript:MonoBehaviour
    {
        private ClickHandlerScript _clickHandlerScript;
        
        [SerializeField] private bool _canInput;
        [SerializeField] private bool _canOutput = true;

        private void Awake()
        {
            _clickHandlerScript = GetComponent<ClickHandlerScript>();
        }

        private void Start()
        {
            _clickHandlerScript.OnHeld += Output;
        }
        
        public bool Input(GameObject dropped)
        {
            return false;
        }

        private void UpdateView(GameObject dropped)
        {
            // Delete all children
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
            
            var gameObjectCopy = Instantiate(dropped.GetComponentInChildren<Canvas>().gameObject, transform);
            var rectTrans = GetComponent<RectTransform>();
            
            gameObjectCopy.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, rectTrans.rect.width);
            gameObjectCopy.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rectTrans.rect.height);
        }

        private void Output()
        {
            return;
        }
    }
}