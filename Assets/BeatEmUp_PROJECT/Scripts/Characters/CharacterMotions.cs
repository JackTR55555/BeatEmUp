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
        float runVel = master.facingRight ? master.moveSpeed : -master.moveSpeed;
        if (master.onAirTime > 0) runVel = runVel * master.airMovilityMultiply;

        float dashV = master.dashSpeed.Evaluate(master.skeletonAnimation.state.GetCurrent(0).AnimationTime);
        float dashVel = master.facingRight ? dashV : -Mathf.Abs(dashV);

        var combo1Vel = master.AttackCores[master.activeAttackCore].anims[0].speedOverTime.Evaluate(master.skeletonAnimation.state.GetCurrent(0).AnimationTime);
        var combo2Vel = master.AttackCores[master.activeAttackCore].anims[1].speedOverTime.Evaluate(master.skeletonAnimation.state.GetCurrent(0).AnimationTime);
        var combo3Vel = master.AttackCores[master.activeAttackCore].anims[2].speedOverTime.Evaluate(master.skeletonAnimation.state.GetCurrent(0).AnimationTime);
        var combo4Vel = master.AttackCores[master.activeAttackCore].anims[3].speedOverTime.Evaluate(master.skeletonAnimation.state.GetCurrent(0).AnimationTime);
        combo1Vel = master.facingRight ? combo1Vel : -Mathf.Abs(combo1Vel);
        combo2Vel = master.facingRight ? combo2Vel : -Mathf.Abs(combo2Vel);
        combo3Vel = master.facingRight ? combo3Vel : -Mathf.Abs(combo3Vel);
        combo4Vel = master.facingRight ? combo4Vel : -Mathf.Abs(combo4Vel);

        var anim = master.animator;

        bool run = (anim.a2 || anim.a4 || anim.a7 || anim.a8 || anim.a9 || anim.a11) && (Mathf.Abs(master.inputs.myLeftStick.x) > 0.2f);
        bool dash = anim.a12;
        bool combo1 = anim.a13;
        bool combo2 = anim.a14;
        bool combo3 = anim.a15;
        bool combo4 = anim.a16;

        if (!run) runVel = 0;
        if (!dash) dashVel = 0;
        if (!combo1) combo1Vel = 0;
        if (!combo2) combo2Vel = 0;
        if (!combo3) combo3Vel = 0;
        if (!combo4) combo4Vel = 0;

        float xVel = dash ? dashVel : combo1 ? combo1Vel : combo2 ? combo2Vel : combo3 ? combo3Vel : combo4 ? combo4Vel : runVel;

        master.body.velocity = new Vector2(xVel, master.body.velocity.y);
    }

    public void MasterJump()
    {
        master.body.velocity = new Vector2(master.body.velocity.x, master.jumpSpeed);
    }

    public void MasterHandleGravity()
    {
        float gravity = master.body.gravityScale;

        if (master.animator.a7 || master.animator.a8)
        {
            if (master.onAirTime <= 0.4f)
            {
                if (master.onAirTime > 0)
                {
                    if (!master.inputs.cross_hold) gravity = master.shortGravity;
                }
                else
                {
                    gravity = master.normalGravity;
                }
            }
            else
            {
                if (master.inputs.cross_hold) gravity = master.normalGravity;
            }
        }
        else
        {
            gravity = master.normalGravity;
        }

        master.body.gravityScale = gravity;

        if (master.body.velocity.y < -MathF.Abs(master.maxFallSpeed))
        {
            master.body.velocity = new Vector2(master.body.velocity.x, -MathF.Abs(master.maxFallSpeed));
        }
    }
}
