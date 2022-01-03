using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace MainGame {
    public class MouseEvents : MonoBehaviour {

        public event Action OnMouseSelectDraggable;
        public event Action OnMouseReleaseSelected;


        public static MouseEvents Instance => instance;
        private static MouseEvents instance;
        public void Awake() {
            if (instance == null) {
                instance = this;
            } else if (instance == this) {
                Destroy(gameObject);
            }
        }





        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                OnMouseSelectDraggable?.Invoke();
            }
            if (Input.GetMouseButtonUp(0)) {
                OnMouseReleaseSelected?.Invoke();
            }
        }


    }
}
