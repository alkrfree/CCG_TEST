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

            DOTween.Kill(cardTransform); // stop all animations

            cardTransform.transform.SetParent(transform);
            
            if (cardTransform.TryGetComponent(out CardHover cardHover)) {
                cardHover.IsPlaying = false;
            }
            cardTransform.SetAsLastSibling();
            cardsOnTable++;
            return true;
        }


    }

}