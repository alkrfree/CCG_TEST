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
        [SerializeField] Transform cardParent;


        // [SerializeField] float testCount;


        [SerializeField] Transform[] points;

        [SerializeField] Transform rotationCenter;

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

            //for (int i = 0; i < testCount; i++) {
            //    var card = cardFactory.SpawnTestCard();
            //    cardsInHand.Add(card);
            //    card.OnDeath += OnCardDeath;
            //}

            cardsInHand = cardsInHand.OrderBy(card => card.cardData.HealthPoints).ToList();

            ArrangeCardsOnArc();
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

            Debug.Log("changeValueCounter = " + changeValueCounter);
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

            if (changeValueCounter + 1 == cardsInHand.Count) {
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
            if (cardsInHand.Contains(card)) {
                cardsInHand.Remove(card);
            } else {
                Debug.LogError("No card in hand");
            }
            if (changeValueCounter == cardsInHand.Count) {
                changeValueCounter--;
            }
            ArrangeCardsOnArc();
        }

        private void OnCardTableDrop(Card card) {
            if (cardsInHand.Contains(card)) {
                cardsInHand.Remove(card);
            } else {
                Debug.LogError("No card in hand");
            }
            if (changeValueCounter == cardsInHand.Count) {
                changeValueCounter--;
            }
            ArrangeCardsOnArc();
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
