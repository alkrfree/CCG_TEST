using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
namespace MainGame {
    public class HandController : MonoBehaviour {

        private CardFactory cardFactory;
        private List<Card> cardsInHand = new List<Card>();
        [SerializeField] private RectTransform MainCanvasTransform;
        [SerializeField] Transform cardParent;

        [SerializeField] Transform[] points;

        [SerializeField] Transform rotationCenter;
        [SerializeField] float cardLineOffsetX = 300f;
        [SerializeField] float cardLineOffsetY = 20f;

        private int changeValueCounter;
        private void Awake() {
            cardFactory = GetComponent<CardFactory>();
        }
        void Start() {
            DragAndDropManager.Instance.OnTableDrop += OnCardTableDrop;

            var cards = CardDataManager.Instance.Cards;
            for (int i = 0; i < cards.Length; i++) {
                var card = cardFactory.SpawnCard(cards[i]);
                cardsInHand.Add(card);
                card.OnDeath += OnCardDeath;
            }

            // make offset adaptive
            cardLineOffsetX *= MainCanvasTransform.localScale.x;
            cardLineOffsetY *= MainCanvasTransform.localScale.y;

            cardsInHand = cardsInHand.OrderBy(card => card.cardData.HealthPoints).ToList();
            ArrangeCards();
        }

        private void ArrangeCards() {
            if (cardsInHand.Count > 3) {
                ArrangeCardsOnArc();
            } else {
                ArrangeCardsOnLine();
            }
        }
        private void ArrangeCardsOnLine() {
            float increment = 1f / (cardsInHand.Count + 1);
            float counter = increment;

            for (int i = 0; i < cardsInHand.Count; i++) {
                var x = Mathf.Lerp(points[0].position.x, points[2].position.x, counter);
                var pos = new Vector3(x + (cardLineOffsetX * i), points[0].position.y + cardLineOffsetY, points[0].position.z);
                cardsInHand[i].PositionInHands = pos;
                cardsInHand[i].RotationInHands = Vector3.zero;
            }
        }
        private void ArrangeCardsOnArc() {
            Vector3 pos;
            Vector3 m1;
            Vector3 m2;
            Quaternion lookRotation;
            float increment = 1f / (cardsInHand.Count + 1);
            float counter = increment;
            for (int i = 0; i < cardsInHand.Count; i++) {
                m1 = Vector3.Lerp(points[0].position, points[1].position, counter);
                m2 = Vector3.Lerp(points[1].position, points[2].position, counter);
                pos = Vector3.Lerp(m1, m2, counter);

                cardsInHand[i].PositionInHands = pos;

                lookRotation = Quaternion.LookRotation(Vector3.forward,
                    (pos - rotationCenter.position).normalized);
                cardsInHand[i].RotationInHands = lookRotation.eulerAngles;

                cardsInHand[i].transform.SetSiblingIndex(i);
                counter += increment;
            }
        }

        public void ChangeRandomValueOfCard() {
            if (cardsInHand.Count == 0) {
                return;
            }

            switch (UnityEngine.Random.Range(0, 3)) {
                case 0:
                    cardsInHand[changeValueCounter].HealthPoints = UnityEngine.Random.Range(-2, 10);
                    break;
                case 1:
                    cardsInHand[changeValueCounter].AttackPoints = UnityEngine.Random.Range(-2, 10);
                    break;
                case 2:
                    cardsInHand[changeValueCounter].ManaPoints = UnityEngine.Random.Range(-2, 10);
                    break;
            }

            if (changeValueCounter + 1 >= cardsInHand.Count) {
                changeValueCounter = 0;
            } else {
                changeValueCounter++;
            }
        }

        public void KillRandomCard() {
            if (cardsInHand.Count == 0) {
                return;
            }
            cardsInHand[UnityEngine.Random.Range(0, cardsInHand.Count)].HealthPoints = -9;
        }
        private void OnCardDeath(Card card) {
            cardFactory.KillCard(card);
            RemoveCardFromHands(card);
        }
        private void OnCardTableDrop(Card card) {
            RemoveCardFromHands(card);
        }
        private void RemoveCardFromHands(Card card) {
            var index = 0;
            if (cardsInHand.Contains(card)) {
                index = cardsInHand.FindIndex(a => a == card);
                cardsInHand.Remove(card);
            } else {
                Debug.LogError("No card in hand");
            }

            if (changeValueCounter > index) {
                changeValueCounter--;
            }

            if (changeValueCounter == cardsInHand.Count) {
                changeValueCounter = 0;
            }
            ArrangeCards();
        }


        private void OnDestroy() {
            DragAndDropManager.Instance.OnTableDrop -= OnCardTableDrop;
        }

        private void OnDrawGizmos() {
            Gizmos.DrawLine(points[0].position, points[1].position);
            Gizmos.DrawLine(points[1].position, points[2].position);

            Gizmos.DrawSphere(rotationCenter.position, 20f);
        }

    }
}
