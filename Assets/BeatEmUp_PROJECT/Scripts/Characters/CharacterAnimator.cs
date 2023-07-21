using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterAnimator : MonoBehaviour
{
    [HideInInspector] public Master master;

    #region Utiles
    [HideInInspector] public bool a1;
    [HideInInspector] public bool a2;
    [HideInInspector] public bool a3;
    [HideInInspector] public bool a4;
    [HideInInspector] public bool a5;
    [HideInInspector] public bool a6;
    [HideInInspector] public bool a7;
    [HideInInspector] public bool a8;
    [HideInInspector] public bool a9;
    [HideInInspector] public bool a10;
    [HideInInspector] public bool a11;
    #endregion

    [Header("Transitions")]
    [SpineAnimation] public string actionIdleToNeutralIdle;
    [SpineAnimation] public string actionIdleToRun;
    [SpineAnimation] public string runToActionIdle;
    [SpineAnimation] public string landToRun;
    [Header("States")]
    [SpineAnimation] public string neutralIdle;
    [SpineAnimation] public string actionIdle;
    [SpineAnimation] public string walk;
    [SpineAnimation] public string run;
    [SpineAnimation] public string jumpUp;
    [SpineAnimation] public string jumpFw;
    [SpineAnimation] public string fall;
    [SpineAnimation] public string land;

    [Space(20)]
    public States currentState = States.neutralIdle;

    void SetNewState(States toState, int layer)
    {
        if (toState == currentState) return;

        switch (toState)
        {
            case States.neutralIdle:
                if (currentState == States.run)
                {
                    master.skeletonAnimation.AnimationState.SetAnimation(layer, runToActionIdle, false);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdleToNeutralIdle, false, 0);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, neutralIdle, true, 0);
                }
                else if (currentState == States.actionIdle)
                {
                    master.skeletonAnimation.AnimationState.SetAnimation(layer, actionIdleToNeutralIdle, false);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, neutralIdle, true, 0);
                }
                else if (currentState == States.land)
                {
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, neutralIdle, true, 0);
                }
                else
                {
                    master.skeletonAnimation.AnimationState.SetAnimation(layer, neutralIdle, true);
                }
                break;
            case States.actionIdle:
                master.skeletonAnimation.AnimationState.SetAnimation(layer, actionIdle, true);
                break;
            case States.walk:
                master.skeletonAnimation.AnimationState.SetAnimation(layer, walk, true);
                break;
            case States.run:
                if (currentState == States.land)
                {
                    master.skeletonAnimation.AnimationState.SetAnimation(layer, landToRun, false);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, run, true, 0);
                }
                else
                {
                    master.skeletonAnimation.AnimationState.SetAnimation(layer, actionIdleToRun, false);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, run, true, 0);
                }
                break;
            case States.jumpUp:
                master.skeletonAnimation.AnimationState.SetAnimation(layer, jumpUp, false);
                master.skeletonAnimation.AnimationState.AddAnimation(layer, fall, true, 0);
                break;
            case States.jumpFw:
                master.skeletonAnimation.AnimationState.SetAnimation(layer, jumpFw, false);
                master.skeletonAnimation.AnimationState.AddAnimation(layer, fall, true, 0);
                break;
            case States.fall:
                master.skeletonAnimation.AnimationState.SetAnimation(layer, fall, true);
                break;
            case States.land:
                master.skeletonAnimation.AnimationState.SetAnimation(layer, land, false);
                break;
            default:
                break;
        }
        currentState = toState;
    }

    public void Animate()
    {
        a1 = master.skeletonAnimation.AnimationState.ToString() == master.animator.actionIdle;
        a2 = master.skeletonAnimation.AnimationState.ToString() == master.animator.actionIdleToRun;
        a3 = master.skeletonAnimation.AnimationState.ToString() == master.animator.neutralIdle;
        a4 = master.skeletonAnimation.AnimationState.ToString() == master.animator.run;
        a5 = master.skeletonAnimation.AnimationState.ToString() == master.animator.runToActionIdle;
        a6 = master.skeletonAnimation.AnimationState.ToString() == master.animator.actionIdleToNeutralIdle;
        a7 = master.skeletonAnimation.AnimationState.ToString() == master.animator.jumpUp;
        a8 = master.skeletonAnimation.AnimationState.ToString() == master.animator.jumpFw;
        a9 = master.skeletonAnimation.AnimationState.ToString() == master.animator.fall;
        a10 = master.skeletonAnimation.AnimationState.ToString() == master.animator.land;
        a11 = master.skeletonAnimation.AnimationState.ToString() == master.animator.landToRun;

        if (master.inputs.myLeftStick.x > 0.2f) master.facingRight = true;
        if (master.inputs.myLeftStick.x < -0.2f) master.facingRight = false;

        #region Run
        if (!a7 && !a8 && !a9)
        {
            if (Mathf.Abs(master.inputs.myLeftStick.x) > 0.2f) SetNewState(States.run, 0);
            if (Mathf.Abs(master.inputs.myLeftStick.x) <= 0.2f) SetNewState(States.neutralIdle, 0);
        }
        #endregion

        #region Jumps
        bool ct = master.onAirTime < master.coyoteTime;
        if (master.grounded)
        {
            if (a1 || a3 || a5 || a6 || a10)
            {
                if (master.inputs.cross_tick) SetNewState(States.jumpUp, 0);
                if (master.inputs.cross_tick) master.currentJump += 1;
            }

            if (a2 || a4 || a11)
            {
                if (master.inputs.cross_tick) SetNewState(States.jumpFw, 0);
                if (master.inputs.cross_tick) master.currentJump += 1;
            }
        }
        else
        {
            if (master.inputs.cross_tick) SetNewState(States.jumpFw, 0);
            if (ct)
            {
                /*if (Mathf.Abs(master.inputs.myLeftStick.x) > 0.2f)
                {
                    if (master.inputs.cross_tick) SetNewState(States.jumpFw, 0);
                    if (master.inputs.cross_tick) master.currentJump += 1;
                }
                else
                {
                    if (master.inputs.cross_tick) SetNewState(States.jumpUp, 0);
                    if (master.inputs.cross_tick) master.currentJump += 1;
                }*/
            }
        }
        #endregion

        #region Falls
        if (!master.grounded && !a7 && !a8) SetNewState(States.fall, 0);
        #endregion

        #region Lands
        if ( ( ( master.onAirTime > 5 && ( a7 || a8 ) ) || a9 ) && master.grounded ) SetNewState(States.land, 0);
        if (master.grounded) master.onAirTime = 0;
        if (master.grounded) master.currentJump = 0;
        #endregion
    }
}
