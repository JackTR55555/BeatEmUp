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
        if (master.onAirTime > 0) runVel.x = runVel.x * master.airMovilityMultiply;

        var vel = master.dashSpeed.Evaluate(master.skeletonAnimation.state.GetCurrent(0).AnimationTime);
        Vector2 dashVel = new Vector2(master.facingRight ? vel : -Mathf.Abs(vel), master.body.velocity.y);

        var anim = master.animator;

        bool cancel = (!anim.a2 && !anim.a4 && !anim.a7 && !anim.a8 && !anim.a9 && !anim.a11) || (Mathf.Abs(master.inputs.myLeftStick.x) <= 0.2f);

        if (cancel) runVel = new Vector2(0, master.body.velocity.y);

        if (!anim.a12) master.body.velocity = runVel;

        if (anim.a12) master.body.velocity = dashVel;
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
            if (master.onAirTime <= 0.2f)
            {
                if (!master.inputs.cross_hold && master.onAirTime > 0) gravity = master.shortGravity;

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
