using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame {
    public class CardDataManager {
        public CardData[] Cards => cards;
        private CardData[] cards;

        private static CardDataManager instance;

        public static CardDataManager Instance {
            get {
                if (instance == null)
                    instance = new CardDataManager();
                return instance;
            }
        }

        public CardData[] InitCardDataByImages(Sprite[] sprites) {
            cards = new CardData[sprites.Length];
            for (int i = 0; i < cards.Length; i++) {
                cards[i] = new CardData();

                cards[i].Title = "Name " + Random.Range(0, 999);
                cards[i].Description = "Desc " + Random.Range(0, 999);
                cards[i].AttackPoints = Random.Range(0, 10); ;
                cards[i].HealthPoints = Random.Range(1, 10); ;
                cards[i].ManaPoints = Random.Range(0, 10);
                cards[i].MainSprite = sprites[i];
            }
            return cards;
        }

    }
}