using UnityEngine;

namespace View.Scripts.MouseHandler
{
    public class DeductionModeClickHandler: ClickHandler<DeductionModeClickHandler>, IClickHandler
    {
        public override IClickHandler HandleClicks(Camera camera)
        {
            Debug.Log("Deduction");
            
            if (Input.GetMouseButtonUp(1))
            {
                // TODO: Clear all pins 
                
                return NormalClickHandler.Instance;
            }

            return Instance;
        }
    }
}