using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotions : MonoBehaviour
{
    [HideInInspector] public Master master;

    public void MasterMoveCharacter(){
        Vector2 vel = new Vector2(master.facingRight ? master.moveSpeed : -master.moveSpeed, master.body.velocity.y);

        var anim = master.animator;

        bool cancel = (!anim.a2 && !anim.a4 && !anim.a7 && !anim.a8 && !anim.a9 && !anim.a11) || (Mathf.Abs(master.inputs.myLeftStick.x) <= 0.2f);

        if (cancel) vel = new Vector2(0, master.body.velocity.y);

        master.body.velocity = vel;
    }

    public void MasterJump()
    {
        bool ct = master.onAirTime < master.coyoteTime;

        if (master.currentJump < master.maxJumps)
        {
            if (master.grounded)
            {
                if (master.inputs.cross_tick) master.body.velocity = new Vector2(master.body.velocity.x, master.jumpSpeed);
            }
            else
            {
                if ( ct )
                {
                    if (master.inputs.cross_tick) master.body.velocity = new Vector2(master.body.velocity.x, master.jumpSpeed);
                }
            }
        }
    }

    public void MasterHandleGravity()
    {
        bool a = (master.animator.a7 || master.animator.a8 || master.animator.a9) && master.body.velocity.y <= master.maxFallSpeed;
        if (a) master.body.velocity = new Vector2(master.body.velocity.x, master.maxFallSpeed);//Clamp
        if (!master.animator.a7 && !master.animator.a8) master.body.gravityScale = master.normalGravity;//Normal Gravity

        if (master.animator.a7 || master.animator.a8)
        {
            if (master.onAirTime <= 10)
            {
                if (!master.inputs.cross_hold)
                {
                    if (master.onAirTime > 0)
                    {
                        master.body.gravityScale = master.shortGravity;
                    }
                }
                else
                {
                    master.body.gravityScale = master.normalGravity;
                }
            }
        }
        else
        {
            master.body.gravityScale = master.normalGravity;
        }
    }
}
