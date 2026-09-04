using BepInEx;
using ModdingUtils.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CupheadSetup : MonoBehaviour {

    // Gun Setup
    public List<CardInfo> cards;
    Player owner;
    int random_number;

    // Super Meter Setup

    void Start()
    {
        owner = GetComponentInParent<Player>();

        random_number = Random.Range(0, cards.Count);

        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(owner, cards[random_number], false, "", 2f, 2f, true);

    }
} 