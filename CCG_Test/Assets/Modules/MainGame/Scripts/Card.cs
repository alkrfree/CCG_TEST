using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace MainGame {
    public class Card : MonoBehaviour, IResetable {
        [HideInInspector] public CardData cardData;

        public TMP_Text Title;
        public TMP_Text Description;

        public TMP_Text AttackPoints;
        public Image AttackIcon;

        public TMP_Text HealthPoints;
        public Image HealthIcon;

        public TMP_Text ManaPoints;
        public Image ManaIcon;

        public Image MainImage;

        public void Reset() {
          
        }
    }
}
//Art + UI overlay
//Title
//Description
//Attack icon + text value
//HP icon + text value
//Mana icon + text value
