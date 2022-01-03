using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace MainGame {
    public class DragAndDropManager : MonoBehaviour {
        public event Card.CardHandler OnTableDrop;
        private CardDragAndDrop selectedObject;
   
        [SerializeField] private GraphicRaycaster raycaster;
        [SerializeField] private EventSystem eventSystem;
        private List<RaycastResult> rayCastResults = new List<RaycastResult>();
        private PointerEventData pointerEventData;
        public static DragAndDropManager Instance => instance;
        private static DragAndDropManager instance;
        public void Awake() {
            if (instance == null) {
                instance = this;
            } else if (instance == this) {
                Destroy(gameObject);
            }
        }

        private void Start() {
            MouseEvents.Instance.OnMouseSelectDraggable += SelectObject;
            MouseEvents.Instance.OnMouseReleaseSelected += DeselectObject;
            pointerEventData = new PointerEventData(eventSystem);
        }
        void Update() {
            if (selectedObject == null) {
                return;
            }
            Vector2 currentMousePosition = Input.mousePosition;
            selectedObject.RectTransform.position = new Vector3(currentMousePosition.x, currentMousePosition.y, selectedObject.RectTransform.position.z);
        }

        private void SelectObject() {
            if (selectedObject != null) return;

            var draggable = UIRaycast<IDraggable>();
            if (draggable == null) return;


            if (draggable is CardDragAndDrop cardDragAndDrop) {
                if (!cardDragAndDrop.IsDraggable || cardDragAndDrop.IsDroppedOnTable) return;
                selectedObject = cardDragAndDrop;
                DOTween.Kill(selectedObject.transform);
                selectedObject.OnBeginDrag();
            }


        }

        private void DeselectObject() {
            if (selectedObject == null) {
                return;
            }
            Debug.Log("Deselect Object");
            var table = UIRaycast<DropTable>();
            if (table != null) {
                if (table.TryAddCard(selectedObject.transform)) {
                    selectedObject.IsDroppedOnTable = true;
                    OnTableDrop?.Invoke(selectedObject.GetComponent<Card>());
                }
            }
            selectedObject.OnEndDrag();
            selectedObject = null;
        }

        private T UIRaycast<T>() where T : class {
            pointerEventData.position = Input.mousePosition;
            raycaster.Raycast(pointerEventData, rayCastResults);
            foreach (RaycastResult result in rayCastResults) {
                if (result.gameObject.TryGetComponent(out T draggable)) {
                    rayCastResults.Clear();
                    return draggable;
                }
            }
            rayCastResults.Clear();
            return null;
        }
        private void OnDestroy() {
            MouseEvents.Instance.OnMouseSelectDraggable -= SelectObject;
            MouseEvents.Instance.OnMouseReleaseSelected -= DeselectObject;
        }
    }
}