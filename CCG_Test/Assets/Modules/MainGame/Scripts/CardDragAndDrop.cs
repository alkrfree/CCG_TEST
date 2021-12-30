using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
namespace MainGame {
    public class CardDragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

        public event Card.CardHandler OnTableDrop;
        private Vector2 lastMousePosition;
        private CanvasGroup canvasGroup;
        // private RectTransform rectTransform;
        //  [SerializeField] private Canvas canvas;
        public bool DroppedOnTable;
        Vector3 cachedRotation;
        Vector3 cachedPosition;

        void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
            //    rectTransform = GetComponent<RectTransform>();

        }

        public void OnDrag(PointerEventData eventData) {
            if (DroppedOnTable) {
                return;
            }
            //   rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            Vector2 currentMousePosition = eventData.position;
            Vector2 diff = currentMousePosition - lastMousePosition;
            RectTransform rect = GetComponent<RectTransform>();

            Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, transform.position.z);
            Vector3 oldPos = rect.position;
            rect.position = newPosition;
            if (!IsRectTransformInsideScreen(rect)) {
                rect.position = oldPos;
            }
            lastMousePosition = currentMousePosition;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (DroppedOnTable) {
                return;
            }
            cachedRotation = transform.rotation.eulerAngles;
            cachedPosition = transform.position;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            canvasGroup.blocksRaycasts = false;
            Debug.Log("Begin Drag");
            lastMousePosition = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (DroppedOnTable) {
                return;
            }
            canvasGroup.blocksRaycasts = true;
            if (!DroppedOnTable) {
                transform.DOMove(cachedPosition, 0.5f);
                transform.DORotate(cachedRotation, 0.5f);
            } else {
                OnTableDrop?.Invoke(GetComponent<Card>());
            }

            Debug.Log("End Drag");
        }

        private bool IsRectTransformInsideScreen(RectTransform rectTransform) {
            bool isInside = false;
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            int visibleCorners = 0;
            Rect rect = new Rect(0, 0, Screen.width, Screen.height);
            foreach (Vector3 corner in corners) {
                if (rect.Contains(corner)) {
                    visibleCorners++;
                }
            }
            if (visibleCorners == 4) {
                isInside = true;
            }
            return isInside;
        }


    }
}
