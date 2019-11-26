using System.Collections.Generic;
using Model.Interfaces;
using UnityEngine;

namespace View.Scripts.MouseHandler
{
    public class DeductionModeClickHandler: ClickHandler<DeductionModeClickHandler>, IClickHandler
    {
        public override void Entered()
        {
            Debug.Log("Entered Deduction");
        }

        public override void Escaped()
        {
            Debug.Log("Exited Deduction");
        }

        public override IClickHandler HandleClicks(Camera camera)
        {
            
            
            
            
            if (Input.GetMouseButtonUp(1))
            {
                // TODO: Clear all pins 
                
                return NormalClickHandler.Instance;
            }

            return Instance;
        }
    }
}