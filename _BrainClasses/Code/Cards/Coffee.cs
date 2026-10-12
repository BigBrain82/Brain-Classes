using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GetComponentInParent<Player>().GetComponentInChildren<CupheadSetup>().sMeter.IncreaseRate();
    }
} 
 