using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame {
    public class GameInitializer : MonoBehaviour {

        private CardFactory cardFactory;
        private void Awake() {
            cardFactory = GetComponent<CardFactory>();
        }
        void Start() {

        }

    }
}
