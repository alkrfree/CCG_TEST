using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MainGame {
    public class DropTable : MonoBehaviour, IDropHandler {
        public void OnDrop(PointerEventData eventData) {
            Debug.Log("OnDrop called.");
            if (eventData.pointerDrag != null) {
                eventData.pointerDrag.GetComponent<CardDragAndDrop>().DroppedOnTable = true;
                eventData.pointerDrag.transform.SetParent(transform);
            }
        }
    }
}