using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;
namespace MainGame {
    public class Card : MonoBehaviour, IResetable {
        public delegate void CardHandler(Card card);
        public event CardHandler OnDeath;

        [HideInInspector] public CardData cardData;

        public TMP_Text Title;
        public TMP_Text Description;

        public TMP_Text AttackPointsText;
        public Image AttackIcon;

        public TMP_Text HealthPointsText;
        public Image HealthIcon;

        public TMP_Text ManaPointsText;
        public Image ManaIcon;

        public Image MainImage;


        private Vector3 positionInHands;
        public Vector3 PositionInHands {
            set {
                positionInHands = value;
                transform.DOMove(value, 1f);
            }
            get {
                return positionInHands;
            }
        }

        private Vector3 rotationInHands;
        public Vector3 RotationInHands {
            set {
                rotationInHands = value;
                transform.DORotate(value, 1f);
            }
            get {
                return rotationInHands;
            }
        }

        public int HealthPoints {
            get {
                return cardData.HealthPoints;
            }
            set {
                DOTween.To(() => cardData.HealthPoints, x => cardData.HealthPoints = x, value, 1).OnUpdate(() => {
                    HealthPointsText.text = cardData.HealthPoints.ToString();
                }).OnComplete(() => {
                    if (cardData.HealthPoints <= 0) {
                        OnDeath?.Invoke(this);
                    }
                });
            }
        }

        public int ManaPoints {
            get {
                return cardData.ManaPoints;
            }
            set {
                DOTween.To(() => cardData.ManaPoints, x => cardData.ManaPoints = x, value, 1).OnUpdate(() => {
                    ManaPointsText.text = cardData.ManaPoints.ToString();
                });
            }
        }

        public int AttackPoints {
            get {
                return cardData.AttackPoints;
            }
            set {
                DOTween.To(() => cardData.AttackPoints, x => cardData.AttackPoints = x, value, 1).OnUpdate(() => {
                    AttackPointsText.text = cardData.AttackPoints.ToString();
                });
            }
        }





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
