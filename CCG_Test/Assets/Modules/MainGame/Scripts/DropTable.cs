using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace MainGame {
    public class DropTable : MonoBehaviour {
        [SerializeField] private int cardLimmit;
        private int cardsOnTable;

        public bool TryAddCard(Transform cardTransform) {
            if (cardsOnTable + 1 > cardLimmit) {
                return false;
            }
            ResetCardGraphics(cardTransform);


            cardTransform.transform.SetParent(transform);
            if (cardTransform.TryGetComponent(out CardHover cardHover)) {
                Destroy(cardHover);
            }
            cardsOnTable++;
            return true;
        }

        private void ResetCardGraphics(Transform cardTransform) {
            cardTransform.rotation = Quaternion.Euler(0, 0, 0);
            cardTransform.localScale = Vector3.one;
            DOTween.Kill(cardTransform);
        }
    }

}