using BepInEx;
using ModdingUtils.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnboundLib.Utils;
using UnityEngine;

public class CupheadSetup : MonoBehaviour
{

    // Gun Setup
    public List<CardInfo> cards;
    Player owner;
    int random_number;

    // Super Meter Setup
    public GameObject SMeter;
    GameObject superMeterObj;
    public SuperMeter sMeter;


    public void Start()
    {
        owner = GetComponentInParent<Player>();

        // Random Gun
        random_number = Random.Range(0, cards.Count);
        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(owner, cards[random_number], false, "", 2f, 2f, true);

        // Super Meter spawn
        Vector3 spawn_position = new Vector3(0, -17, 0);
        superMeterObj = Instantiate(SMeter, spawn_position, Quaternion.identity);
        sMeter = superMeterObj.GetComponent<SuperMeter>();
        sMeter.player = owner;
    }


}