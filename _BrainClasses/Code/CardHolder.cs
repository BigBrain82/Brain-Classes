using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnboundLib.Cards;

public class CardHolder : MonoBehaviour {


    public List<CardInfo> cards;
    public List<CardInfo> hiddenCards;

    internal void RegisterCards() {
        foreach (var card in cards) {
            CustomCard.RegisterUnityCard(card.gameObject, Plugin.modInitials, card.cardName, true, null);
        }
        foreach (var card in hiddenCards) {
            CustomCard.RegisterUnityCard(card.gameObject, Plugin.modInitials, card.cardName, false, null);
            ModdingUtils.Utils.Cards.instance.AddHiddenCard(card);
        }
    }
}