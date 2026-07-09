using BepInEx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BepInDependency("com.willis.rounds.unbound")]
[BepInDependency("pykess.rounds.plugins.moddingutils")]
[BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
[BepInDependency("root.classes.manager.reborn")]
[BepInPlugin("brain.classes.mod", "Brain Classes", "1.1.0")]
[BepInProcess("Rounds.exe")]

public class Plugin : BaseUnityPlugin {

    internal static string modInitials = "BC";
    internal static AssetBundle asset;
    void Awake() {
        asset = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("brainassets", typeof(Plugin).Assembly);
    }
    void Start() {
        asset.LoadAsset<GameObject>("ModCards").GetComponent<CardHolder>().RegisterCards();
    }
} 