using BepInEx;
using System.Collections;
using System.Collections.Generic;
using UnboundLib.Utils.UI;
using UnityEngine;
using ToggleCardsCategories;

[BepInDependency("com.willis.rounds.unbound")]
[BepInDependency("pykess.rounds.plugins.moddingutils")]
[BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
[BepInDependency("root.classes.manager.reborn")]
[BepInDependency("com.aalund13.rounds.toggle_cards_categories")]


[BepInPlugin("brain.classes.mod", "Brain Classes", "1.0.10")]
[BepInProcess("Rounds.exe")]

public class Plugin : BaseUnityPlugin {

    internal static string modInitials = "BC";
    internal static AssetBundle asset;
    void Awake() {
        asset = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("brainassets", typeof(Plugin).Assembly);
        ToggleCardsCategoriesManager.instance.RegisterCategories(modInitials);
    }
    void Start() {
        asset.LoadAsset<GameObject>("ModCards").GetComponent<CardHolder>().RegisterCards();
    }
}