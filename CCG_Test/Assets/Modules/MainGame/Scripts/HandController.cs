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


        [SerializeField] float testCount;


        [SerializeField] Transform[] points;

        [SerializeField] Transform rotationCenter;
        private void Awake() {
            cardFactory = GetComponent<CardFactory>();
        }
        void Start() {
            var cards = CardDataManager.Instance.Cards;
            //for (int i = 0; i < cards.Length; i++) {
            //    cardsInHand.Add(cardFactory.SpawnCard(cards[i]));
            //}

            for (int i = 0; i < testCount; i++) {
                cardsInHand.Add(cardFactory.SpawnTestCard());
            }
            cardsInHand.OrderBy(card => card.HealthPoints);
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

                cardsInHand[i].transform.position = pos;

                lookRotation = Quaternion.LookRotation(Vector3.forward,
                    (cardsInHand[i].transform.position - rotationCenter.position).normalized);
                cardsInHand[i].transform.rotation = lookRotation;

                cardsInHand[i].transform.SetAsLastSibling();

                counter += increment;
            }



        }
        private void OnDrawGizmos() {
            Gizmos.DrawLine(points[0].position, points[1].position);
            Gizmos.DrawLine(points[1].position, points[2].position);

            Gizmos.DrawSphere(rotationCenter.position, 20f);
        }

    }
}
