using Photon.Pun;
using SimulationChamber;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnboundLib;
using UnityEngine;
using WeaponsManager;

public enum ExGuns
{
    None,
    PeaEX,
    ChaserEX,
    LobberEX,
    SpreadEX,
    RoundEX,
    ConvergeEX,
    CrackEX,
    TwistEX,
    ChargeEX
}

public class EX : MonoBehaviour
{
    Player player;
    WeaponManager weaponManager;
    public static Dictionary<ExGuns, SimulatedGun> exSimulatedGuns = new Dictionary<ExGuns, SimulatedGun>();

    // Sounds
    private AudioSource gunAudio;

    public AudioClip peaSound;
    public AudioClip chaserSound;
    public AudioClip lobberSound;
    public AudioClip spreadSound;
    public AudioClip roundSound;
    public AudioClip convergeSound;
    public AudioClip crackSound;
    public AudioClip twistSound;
    public AudioClip chargeSound;
     
    public void Start()
    {
        player = GetComponentInParent<Player>();
        gunAudio = gameObject.AddComponent<AudioSource>();
        weaponManager = gameObject.GetComponentInParent<WeaponManager>();
        GetComponentInParent<ChildRPC>().childRPCsInt.Add("EXShootGun", RPCA_EXShootGun);

        exSimulatedGuns[ExGuns.PeaEX] = new GameObject("PeaEX").AddComponent<SimulatedGun>();
        exSimulatedGuns[ExGuns.ChaserEX] = new GameObject("ChaserEX").AddComponent<SimulatedGun>();
        exSimulatedGuns[ExGuns.LobberEX] = new GameObject("LobberEX").AddComponent<SimulatedGun>();
        exSimulatedGuns[ExGuns.SpreadEX] = new GameObject("SpreadEX").AddComponent<SimulatedGun>();
        exSimulatedGuns[ExGuns.RoundEX] = new GameObject("RoundEX").AddComponent<SimulatedGun>();
        exSimulatedGuns[ExGuns.ConvergeEX] = new GameObject("ConvergeEX").AddComponent<SimulatedGun>();
        exSimulatedGuns[ExGuns.CrackEX] = new GameObject("CrackEX").AddComponent<SimulatedGun>();
        exSimulatedGuns[ExGuns.TwistEX] = new GameObject("TwistEX").AddComponent<SimulatedGun>();
        exSimulatedGuns[ExGuns.ChargeEX] = new GameObject("ChargeEX").AddComponent<SimulatedGun>();
    }

    public bool ExUsed()
    {
        ExGuns exGun = GetExGun();
        if (exGun != ExGuns.None)
        {
            // Do EX guns stuffs here :D
            GetComponentInParent<ChildRPC>().CallFunction("EXShootGun", (int)exGun);
            return true;
        }
        // The gun is not a cuphead gun
        return false;
    }
    private void RPCA_EXShootGun(int enumIndex)
    {
        ExGuns gun = (ExGuns)enumIndex;
        SimulatedGun exGun = exSimulatedGuns[gun];

        // Copies base gun stats
        exGun.CopyGunStatsExceptActions(player.data.weaponHandler.gun);
        exGun.CopyAttackAction(player.data.weaponHandler.gun);
        exGun.CopyShootProjectileAction(player.data.weaponHandler.gun);


        if (!(player.data.view.IsMine || PhotonNetwork.OfflineMode))
        {
            return;
        }

        // Fires gun
        switch ((ExGuns)enumIndex)
        {
            // Peashooter EX - Large projectile with more damage
            case ExGuns.PeaEX:
                exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 5);

                gunAudio.PlayOneShot(peaSound, 3f);
                break;
            // Chaser EX - A circle of 8 bullets around the player
            case ExGuns.ChaserEX:
                exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 3);

                gunAudio.PlayOneShot(chaserSound, 1f);
                break;
            // Lobber EX - Large projectile with more damage, increases gravity
            case ExGuns.LobberEX:
                exGun.gravity *= 2f;
                exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 4);

                gunAudio.PlayOneShot(lobberSound, 1f);
                break;
            // Roundabout EX - Back and forth
            case ExGuns.RoundEX:
                exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 3);

                gunAudio.PlayOneShot(roundSound, 1.2f);
                break;
            // Twist-Up EX
            case ExGuns.TwistEX:
                exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 3);

                gunAudio.PlayOneShot(twistSound, 0.5f);
                break;
            // Spread EX - 8-Way even shot
            case ExGuns.SpreadEX:
                exGun.numberOfProjectiles = 1;
                exGun.spread = 1f;
                exGun.evenSpread = 1f;
                exGun.gravity *= 0f;
                exGun.destroyBulletAfter = 0;
                var spawn_x = transform.position.x;
                var spawn_y = transform.position.y;
                var spawn_z = transform.position.z;
                Vector3 bulletSpawn1 = new Vector3(spawn_x, spawn_y, spawn_z);
                Vector3 bulletSpawn3 = new Vector3(-spawn_x, spawn_y, spawn_z);
                Vector3 bulletSpawn5 = new Vector3(spawn_x, -spawn_y, spawn_z);
                Vector3 bulletSpawn7 = new Vector3(-spawn_x, -spawn_y, spawn_z);
                exGun.SimulatedAttack(this.player.playerID, bulletSpawn1, player.data.aimDirection, 1f, 5); // 0
                //exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 5); // 45
                //exGun.SimulatedAttack(this.player.playerID, bulletSpawn3, player.data.aimDirection, 1f, 5); // 90
                //exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 5); // 135
                //exGun.SimulatedAttack(this.player.playerID, bulletSpawn5, player.data.aimDirection, 1f, 5); // 180
                //exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 5); // 225
                //exGun.SimulatedAttack(this.player.playerID, bulletSpawn7, player.data.aimDirection, 1f, 5); // 270
                //exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 5); // 315

                gunAudio.PlayOneShot(spreadSound, 1.5f);
                break;  
            // Converge EX - Single beam of lightning
            case ExGuns.ConvergeEX:
                exGun.projectileSpeed *= 1.35f;
                exGun.spread *= 0;
                exGun.evenSpread *= 0;
                exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 2);

                gunAudio.PlayOneShot(convergeSound, 0.8f);
                break;
            // Charge EX - Explosion that doesn't damage player
            case ExGuns.ChargeEX:
                exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 4);

                gunAudio.PlayOneShot(chargeSound, 0.5f);
                break;
            // Crack EX - Spawns a turrent that shoots at closest enemy
            case ExGuns.CrackEX:
                
                exGun.SimulatedAttack(this.player.playerID, transform.position, player.data.aimDirection, 1f, 3);

                gunAudio.PlayOneShot(crackSound, 0.5f);
                break;
        }
    }

    private ExGuns GetExGun()
    {
        if (weaponManager.GetWeapon("PeashooterGun") && (weaponManager.GetWeapon("PeashooterGun") == player.data.weaponHandler.gun))
        {
            return ExGuns.PeaEX;
        }
        else if (weaponManager.GetWeapon("ChaserGun") && (weaponManager.GetWeapon("ChaserGun") == player.data.weaponHandler.gun))
        {
            return ExGuns.ChaserEX;
        }
        else if (weaponManager.GetWeapon("LobberGun") && (weaponManager.GetWeapon("LobberGun") == player.data.weaponHandler.gun))
        {
            return ExGuns.LobberEX;
        }
        else if (weaponManager.GetWeapon("SpreadGun") && (weaponManager.GetWeapon("SpreadGun") == player.data.weaponHandler.gun))
        {
            return ExGuns.SpreadEX;
        }
        else if (weaponManager.GetWeapon("RoundGun") && (weaponManager.GetWeapon("RoundGun") == player.data.weaponHandler.gun))
        {
            return ExGuns.RoundEX;
        }
        else if (weaponManager.GetWeapon("ConvergeGun") && (weaponManager.GetWeapon("ConvergeGun") == player.data.weaponHandler.gun))
        {
            return ExGuns.ConvergeEX;
        }
        else if (weaponManager.GetWeapon("CrackshotGun") && (weaponManager.GetWeapon("CrackshotGun") == player.data.weaponHandler.gun))
        {
            return ExGuns.CrackEX;
        }
        else if (weaponManager.GetWeapon("TwistUpGun") && (weaponManager.GetWeapon("TwistUpGun") == player.data.weaponHandler.gun))
        {
            return ExGuns.TwistEX;
        }
        else if (weaponManager.GetWeapon("ChargeGun") && (weaponManager.GetWeapon("ChargeGun") == player.data.weaponHandler.gun))
        {
            return ExGuns.ChargeEX;
        }

        return ExGuns.None;
    }


    private void OnDestroy()
    {
        GetComponentInParent<ChildRPC>().childRPCsInt.Remove("EXShootGun");
    }

}
