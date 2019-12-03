using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Extensions
{
    public static class Extensions
    {
        public static Vector3 GetPointBeneathMouse(this Camera camera)
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var raycastResult, 30))
            {
                return raycastResult.point;
            }

            return Vector3.zero;
        }
        
        public static bool GetElementBeneathMouse(this Camera camera, out GameObject[] results, bool includeUI = false)
        {
            RaycastHit raycastResult;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            
            if (includeUI)
            {
                var pointerEventData = new PointerEventData(EventSystem.current) {position = Input.mousePosition};

                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                if (raycastResults.Count > 0)
                {
                    results = new GameObject[raycastResults.Count];

                    for (int i = 0; i < raycastResults.Count; i++)
                    {
                        results[i] = raycastResults[i].gameObject;
                    }

                    return true;
                }
            }

            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out raycastResult, 30))
            {
                results = new [] {
                    raycastResult.collider.gameObject
                };
                
                return true;
            }

            results = new GameObject[0];
            return false;
        }

        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }
    }
}