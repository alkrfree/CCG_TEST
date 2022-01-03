using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MainGame {
    public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] GameObject shineImage;
        int cachedSbiling;
        private bool isPlaying = true;
        public bool IsPlaying {
            set {
                isPlaying = value;
                if (!isPlaying) {
                    shineImage.SetActive(false);
                    transform.localScale = Vector3.one;
                    transform.SetSiblingIndex(cachedSbiling);
                }
            }
            get {
                return isPlaying;
            }
        }
        public void OnPointerEnter(PointerEventData eventData) {
            if (!isPlaying) return;
            shineImage.SetActive(true);
            transform.localScale *= 1.2f;
            cachedSbiling = transform.GetSiblingIndex();
            transform.SetSiblingIndex(999);
        }

        public void OnPointerExit(PointerEventData eventData) {
            if (!isPlaying) return;
            shineImage.SetActive(false);
            transform.localScale = Vector3.one;
            transform.SetSiblingIndex(cachedSbiling);
        }

    }
}
