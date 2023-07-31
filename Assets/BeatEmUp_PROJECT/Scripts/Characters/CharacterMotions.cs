using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotions : MonoBehaviour
{
    [HideInInspector] public Master master;

    [Header("Events")]
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string jumpEvent;
    public Spine.EventData jumpEventData;

    public void MasterMoveCharacter(){
        Vector2 runVel = new Vector2(master.facingRight ? master.moveSpeed : -master.moveSpeed, master.body.velocity.y);
        Vector2 dashVel = new Vector2(master.facingRight ? master.dashSpeed : -Mathf.Abs(master.dashSpeed), master.body.velocity.y);

        var anim = master.animator;

        bool cancel = (!anim.a2 && !anim.a4 && !anim.a7 && !anim.a8 && !anim.a9 && !anim.a11) || (Mathf.Abs(master.inputs.myLeftStick.x) <= 0.2f);

        if (cancel) runVel = new Vector2(0, master.body.velocity.y);

        if (!anim.a12)
        {
            master.body.velocity = runVel;
        }
        else
        {
            master.body.velocity = dashVel;
        }
    }

    public void AnimationEventJump()
    {
        master.body.velocity = new Vector2(master.body.velocity.x, master.jumpSpeed);
    }

    public void MasterHandleGravity()
    {
        bool a = (master.animator.a7 || master.animator.a8 || master.animator.a9) && master.body.velocity.y <= -MathF.Abs(master.maxFallSpeed);
        if (a) master.body.velocity = new Vector2(master.body.velocity.x, -MathF.Abs(master.maxFallSpeed));//Clamp
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
