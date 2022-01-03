using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
namespace MainGame {
    public class CardDragAndDrop : MonoBehaviour, IDraggable {
        public RectTransform RectTransform => rectTransform;

        public bool IsDraggable { get => isDraggable; }
        private bool isDraggable = true;

        public bool IsDroppedOnTable { get => isDroppedOnTable; set => isDroppedOnTable = value; }
        private bool isDroppedOnTable;
        private RectTransform rectTransform; 

        private bool isMoveTweenFinised;
        private bool isRotateTweenFinished;
        private Card card;
        void Awake() {
            rectTransform = GetComponent<RectTransform>();
            card = GetComponent<Card>();
        }

        public void OnDrag() {

        }

        public void OnBeginDrag() {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Begin Drag");

        }

        public void OnEndDrag() {
            if (!IsDroppedOnTable) {
                ReturnCardBackAnimation();
            }
            Debug.Log("End Drag");
        }

        private void ReturnCardBackAnimation() {
            isMoveTweenFinised = false;
            isRotateTweenFinished = false;
            isDraggable = false;

            transform.DOMove(card.PositionInHands, 0.5f).OnComplete(() => {
                isMoveTweenFinised = true;
                IsTweenEnded();
            });

            transform.DORotate(card.RotationInHands, 0.5f).OnComplete(() => {
                isRotateTweenFinished = true;
                IsTweenEnded();
            });
        }
        private void IsTweenEnded() {
            isDraggable = isMoveTweenFinised && isRotateTweenFinished;
        }
    }
}
