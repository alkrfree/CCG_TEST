using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame {
    public class CardFactory : MonoBehaviour {

        //   [SerializeField] private Card cardPrefab;
        // [SerializeField] private Transform cardParent;

        private GameObjectPool cardObjectPool;
        void Awake() {
            cardObjectPool = GetComponent<GameObjectPool>();
        }


        public Card SpawnCard(CardData cardData) {
            var card = cardObjectPool.GetObjectFromPool().GetComponent<Card>();

            card.Title.text = cardData.Title;
            card.Description.text = cardData.Description;
            card.AttackPointsText.text = cardData.AttackPoints.ToString();

            card.HealthPointsText.text = cardData.HealthPoints.ToString();
            card.ManaPointsText.text = cardData.ManaPoints.ToString();
            card.MainImage.sprite = cardData.MainSprite;
            card.cardData = cardData;
            return card;
        }

        public void KillCard(Card card) {
            cardObjectPool.PoolObject(card.gameObject);
        }

        public Card SpawnTestCard() {
            var card = cardObjectPool.GetObjectFromPool().GetComponent<Card>();
            return card;
        }
    }


}


