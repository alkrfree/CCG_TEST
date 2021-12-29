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
        [SerializeField] float arcRadius;
        [SerializeField] float horizontalOffset;
        [SerializeField] float veticalOffset;

        [SerializeField] Transform point1;
        [SerializeField] Transform point2;
        [SerializeField] Transform point3;
        [SerializeField] Transform rotationCenter;
        private void Awake() {
            cardFactory = GetComponent<CardFactory>();
        }
        void Start() {
            var cards = CardDataManager.Instance.Cards;
            //for (int i = 0; i < cards.Length; i++) {
            //    cardsInHand.Add(cardFactory.SpawnCard(cards[i]));
            //}
            for (int i = 0; i < 6; i++) {
                cardsInHand.Add(cardFactory.SpawnTestCard());
            }
            cardsInHand.OrderBy(card => card.HealthPoints);
            ArrangeCardsOnArc();
        }

        private void ArrangeCardsOnArc() {
            int middleId = (int)Math.Ceiling(cardsInHand.Count / 2f);
            var pos = cardParent.position;
            //  cardsInHand[middleId].transform.position = new Vector3(pos.x, pos.y + arcRadius, pos.z);

            //float currentCardOffset = horizontalOffset;
            //var offsetCounter = currentCardOffset;

            for (int i = 0; i < cardsInHand.Count; i++) {
                //   pos = cardParent.position;


                float posX = pos.x - horizontalOffset * ((middleId) - i);

                float posY = pos.y + arcRadius - veticalOffset * Mathf.Abs((middleId - i));
                Debug.Log("posY = " + posY);
                cardsInHand[i].transform.position = new Vector3(posX, posY, pos.z);
                //currentCardOffset += offsetCounter;



                var _direction = (cardParent.position - cardsInHand[i].transform.position).normalized * -1;
                var _lookRotation = Quaternion.LookRotation(Vector3.forward, _direction);

                cardsInHand[i].transform.rotation = _lookRotation;

                cardsInHand[i].transform.SetAsLastSibling();

            }


            //for (int i = middleId - 1; i >= 0; i--) {
            //    pos = cardParent.position;
            //    cardsInHand[i].transform.position = new Vector3(pos.x - currentCardOffset, pos.y + arcRadius, pos.z);
            //    currentCardOffset += offsetCounter;
            //}

            //currentCardOffset = zeroDegreeOffset;
            //for (int i = middleId + 1; i < cardsInHand.Count; i++) {
            //    pos = cardParent.position;
            //    cardsInHand[i].transform.position = new Vector3(pos.x + currentCardOffset, pos.y + arcRadius, pos.z);
            //    currentCardOffset += offsetCounter;
            //}










            //var pos = cardParent.position;

            //var degrees = (180f - zeroDegreeOffset) / cardsInHand.Count;
            //var degreesCounter = degrees;

            //Debug.Log(degrees);
            //for (int i = 0; i < cardsInHand.Count; i++) {

            //    float x = pos.x + arcRadius * Mathf.Cos(degrees * Mathf.Deg2Rad);
            //    float y = pos.y + arcRadius * Mathf.Sin(degrees * Mathf.Deg2Rad);
            //    cardsInHand[i].transform.position = new Vector3(x, y, pos.z);

            //    var _direction = (cardParent.position - cardsInHand[i].transform.position).normalized * -1;
            //    var _lookRotation = Quaternion.LookRotation(Vector3.forward, _direction);

            //    cardsInHand[i].transform.rotation = _lookRotation;

            //    degrees += degreesCounter;
            //    cardsInHand[i].transform.SetAsFirstSibling();

            //}

            //Debug.Log("middleId = " + middleId);
        }
        private void OnDrawGizmos() {
            Gizmos.DrawLine(point1.position, point2.position);
            Gizmos.DrawLine(point2.position, point3.position);

            Gizmos.DrawSphere(rotationCenter.position, 20f);
        }

    }
}
