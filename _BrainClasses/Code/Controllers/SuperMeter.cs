using ModdingUtils.Utils;
using System.Collections;
using System.Collections.Generic;
using UnboundLib;
using UnityEngine;

public class SuperMeter : MonoBehaviour
{

    // General variables
    public float super_rate;
    float filled_amount = 0;
    Vector3 bottom_pos = new Vector3(0, -766);
    Vector3 top_pos = new Vector3(0, 0);
    bool SuperUnlocked = false;

    // Object definitions
    public List<GameObject> ex;
    public GameObject empty_ex;
    public Player player;
    public EX EX;

    void Start()
    {
        GameObject Empty = Instantiate(empty_ex);
        EX = player.GetComponentInChildren<EX>();
        super_rate = 0.10f; // Base rate: 0.10f
    }

    private void Update()
    {   
        // When Control is released, check EX Conditions
        if (Input.GetKeyUp(KeyCode.LeftControl) && filled_amount >= 1)
        {
            if (filled_amount == 5 && SuperUnlocked)
            {
                SuperUsed();
            }
            else
            {
                ExUsed();
            }
        }

        // Passive Super Meter gain
        ExFilling();
        VisualUpdate();
    }

    // Resets Super Meter on Round End and stops passives


    // Enables Super Meter on Round Start and passives



    // Controls gain after dealing damage
    public void ExGain()
    {
        filled_amount += 0.40f; // Base: 0.40f
    }


    private void VisualUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            if (filled_amount < i)
            {
                ex[i].transform.localPosition = bottom_pos;
            }
            else if (filled_amount > i + 1)
            {
                ex[i].transform.localPosition = top_pos;
            }
            else
            {
                ex[i].transform.localPosition = (1 - (filled_amount - i)) * bottom_pos;
            }
        }
    }

    // Controls getting a parry
    private void ParryHit()
    {

    }
    // Controls gradual filling
    private void ExFilling()
    {

        filled_amount += super_rate * Time.deltaTime;

        if (filled_amount > 5)
        {
            filled_amount = 5;
        }
        
    }

    public void IncreaseRate()
    {
        super_rate *= 1.33f; // Base increase: 1.33f aka 33%
        print("Increase rate being called");
        print(super_rate); 
    }

    // Controls using an EX
    private void ExUsed()
    {
        if (EX.ExUsed())
        {
            filled_amount--;
        }
        print(EX.ExUsed());
    } 

    // Controls using a Super
    private void SuperUsed()
    {
        filled_amount = 0;
    }
}   