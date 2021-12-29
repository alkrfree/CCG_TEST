using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MainGame {
    public class CardData : ScriptableObject {
        public string Title;
        public string Description;

        public int AttackPoints;
        //  public Texture2D AttackIcon;

        public int HealthPoints;
        //   public Texture2D HealthIcon;

        public int ManaPoints;
        // public Texture2D ManaIcon;

        public Sprite MainSprite;

    }

    //Art + UI overlay
    //Title
    //Description
    //Attack icon + text value
    //HP icon + text value
    //Mana icon + text value
}
