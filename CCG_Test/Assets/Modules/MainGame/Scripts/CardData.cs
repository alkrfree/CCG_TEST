using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MainGame {
    public class CardData : ScriptableObject {
        public string Title;
        public string Description;

        public int AttackPoints;
        public int HealthPoints;
        public int ManaPoints;

        public Sprite MainSprite;

    }


}
