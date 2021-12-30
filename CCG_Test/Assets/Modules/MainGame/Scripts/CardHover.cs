using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MainGame {
    public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        // Start is called before the first frame update
        int cachedSbiling;
        public void OnPointerEnter(PointerEventData eventData) {
            transform.localScale *= 1.2f;
            cachedSbiling = transform.GetSiblingIndex();
            transform.SetSiblingIndex(999);
        }

        public void OnPointerExit(PointerEventData eventData) {
            transform.localScale = Vector3.one;
            transform.SetSiblingIndex(cachedSbiling);
        }
    }
}
