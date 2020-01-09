using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace A3.Utilities.EventSystems
{
    public static class EventSystemExtension
    {
        public static bool IsPointerOverUiObject(this EventSystem eventSys)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(eventSys)
            {
                position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
            };
            List<RaycastResult> results = new List<RaycastResult>();
            eventSys.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}