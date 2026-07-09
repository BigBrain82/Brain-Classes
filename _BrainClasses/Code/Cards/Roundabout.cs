using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnboundLib.Cards;

public class Roundabout : MonoBehaviour {

    MoveTransform move;
    float bull_timer;
    Vector3 bullet_xspd;
    bool going_right;

    void Start()
    {
        move = base.GetComponentInParent<MoveTransform>();
        bull_timer = 0.3f;
        bullet_xspd = move.velocity += Vector3.left * 1;
        if (bullet_xspd.x < 0)
        {
            going_right = true;
        }
    }

    private void FixedUpdate()
    {
        bull_timer -= Time.deltaTime;

        if (going_right)
        {
            move.velocity += Vector3.left * bull_timer;
        }
        else
        {
            move.velocity += Vector3.right * bull_timer;
        }

    }
}
