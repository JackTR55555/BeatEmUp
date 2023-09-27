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
    [HideInInspector] public bool a12;
    [HideInInspector] public bool a13;
    [HideInInspector] public bool a14;
    [HideInInspector] public bool a15;
    [HideInInspector] public bool a16;
    #endregion

    [Header("Events")]
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string orientateEvent;
    public Spine.EventData orientateEventData;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string cameraShakeEvent;
    public Spine.EventData cameraShakeEventData;
    [Header("Transitions")]
    [SpineAnimation] public string actionIdleToNeutralIdle;
    [SpineAnimation] public string actionIdleToRun;
    [SpineAnimation] public string runToActionIdle;
    [SpineAnimation] public string landToRun;
    [Header("Standard Animations")]
    [SpineAnimation] public string neutralIdle;
    [SpineAnimation] public string actionIdle;
    [SpineAnimation] public string walk;
    [SpineAnimation] public string run;
    [SpineAnimation] public string jumpUp;
    [SpineAnimation] public string jumpFw;
    [SpineAnimation] public string fall;
    [SpineAnimation] public string land;
    [SpineAnimation] public string dash;

    [Space(20)]
    public States currentState = States.neutralIdle;

    void SetNewState(States toState, int layer)
    {
        if (toState == currentState) return;

        switch (toState)
        {
            case States.neutralIdle:
                #region
                if (currentState == States.run)
                {
                    master.skeletonAnimation.AnimationState.SetAnimation(layer, runToActionIdle, false);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdle, true, 0);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdleToNeutralIdle, false, 3);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, neutralIdle, true, 0);
                }
                else if (currentState == States.dash)
                {
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdle, true, 0);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdleToNeutralIdle, false, 3);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, neutralIdle, true, 0);
                }
                else if (currentState == States.land)
                {
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, neutralIdle, true, 0);
                }
                else if (currentState == States.Combo1)
                {
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdle, true, 0);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdleToNeutralIdle, false, 3);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, neutralIdle, true, 0);
                }
                else if (currentState == States.Combo2)
                {
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdle, true, 0);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdleToNeutralIdle, false, 3);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, neutralIdle, true, 0);
                }
                else if (currentState == States.Combo3)
                {
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdle, true, 0);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdleToNeutralIdle, false, 3);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, neutralIdle, true, 0);
                }
                else if (currentState == States.Combo4)
                {
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdle, true, 0);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdleToNeutralIdle, false, 3);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, neutralIdle, true, 0);
                }
                else
                {
                    master.skeletonAnimation.AnimationState.SetAnimation(layer, neutralIdle, true);
                }
                #endregion
                break;
            case States.actionIdle:
                #region
                master.skeletonAnimation.AnimationState.SetAnimation(layer, actionIdle, true);
                #endregion
                break;
            case States.walk:
                #region
                master.skeletonAnimation.AnimationState.SetAnimation(layer, walk, true);
                #endregion
                break;
            case States.run:
                #region
                if (currentState == States.land)
                {
                    master.skeletonAnimation.AnimationState.SetAnimation(layer, landToRun, false);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, run, true, 0);
                }
                else if (currentState == States.actionIdle)
                {
                    master.skeletonAnimation.AnimationState.SetAnimation(layer, actionIdleToRun, false);
                    master.skeletonAnimation.AnimationState.AddAnimation(layer, run, true, 0);
                }
                else
                {
                    master.skeletonAnimation.AnimationState.SetAnimation(layer, run, true);
                }
                #endregion
                break;
            case States.jumpUp:
                #region
                master.skeletonAnimation.AnimationState.SetAnimation(layer, jumpUp, false);
                master.skeletonAnimation.AnimationState.AddAnimation(layer, fall, true, 0);
                #endregion
                break;
            case States.jumpFw:
                #region
                master.skeletonAnimation.AnimationState.SetAnimation(layer, jumpFw, false);
                master.skeletonAnimation.AnimationState.AddAnimation(layer, fall, true, 0);
                #endregion
                break;
            case States.fall:
                #region
                master.skeletonAnimation.AnimationState.SetAnimation(layer, fall, true);
                #endregion
                break;
            case States.land:
                #region
                master.skeletonAnimation.AnimationState.SetAnimation(layer, land, false);
                #endregion
                break;
            case States.dash:
                #region
                master.skeletonAnimation.AnimationState.SetAnimation(layer, dash, false);
                master.skeletonAnimation.AnimationState.AddAnimation(layer, runToActionIdle, false, 0);
                #endregion
                break;
            case States.Combo1:
                #region
                master.skeletonAnimation.AnimationState.SetAnimation(layer, master.AttackCores[master.activeAttackCore].anims[0].anim, false);
                master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdle, false, 0);
                #endregion
                break;
            case States.Combo2:
                #region
                master.skeletonAnimation.AnimationState.SetAnimation(layer, master.AttackCores[master.activeAttackCore].anims[1].anim, false);
                master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdle, false, 0);
                #endregion
                break;
            case States.Combo3:
                #region
                master.skeletonAnimation.AnimationState.SetAnimation(layer, master.AttackCores[master.activeAttackCore].anims[2].anim, false);
                master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdle, false, 0);
                #endregion
                break;
            case States.Combo4:
                #region
                master.skeletonAnimation.AnimationState.SetAnimation(layer, master.AttackCores[master.activeAttackCore].anims[3].anim, false);
                master.skeletonAnimation.AnimationState.AddAnimation(layer, actionIdle, false, 0);
                #endregion
                break;
            default:
                break;
        }
        currentState = toState;
    }

    public void RecalculateFacing()
    {
        if (master.inputs.myLeftStick.x > 0.2f) master.facingRight = true;
        if (master.inputs.myLeftStick.x < -0.2f) master.facingRight = false;
    }

    public void Animate()
    {
        //Standard
        a1 = master.skeletonAnimation.AnimationState.ToString()     == actionIdle;
        a2 = master.skeletonAnimation.AnimationState.ToString()     == actionIdleToRun;
        a3 = master.skeletonAnimation.AnimationState.ToString()     == neutralIdle;
        a4 = master.skeletonAnimation.AnimationState.ToString()     == run;
        a5 = master.skeletonAnimation.AnimationState.ToString()     == runToActionIdle;
        a6 = master.skeletonAnimation.AnimationState.ToString()     == actionIdleToNeutralIdle;
        a7 = master.skeletonAnimation.AnimationState.ToString()     == jumpUp;
        a8 = master.skeletonAnimation.AnimationState.ToString()     == jumpFw;
        a9 = master.skeletonAnimation.AnimationState.ToString()     == fall;
        a10 = master.skeletonAnimation.AnimationState.ToString()    == land;
        a11 = master.skeletonAnimation.AnimationState.ToString()    == landToRun;
        a12 = master.skeletonAnimation.AnimationState.ToString()    == dash;
        //Sword Combos
        a13 = master.skeletonAnimation.AnimationState.ToString()    == master.AttackCores[master.activeAttackCore].anims[0].anim;
        a14 = master.skeletonAnimation.AnimationState.ToString()    == master.AttackCores[master.activeAttackCore].anims[1].anim;
        a15 = master.skeletonAnimation.AnimationState.ToString()    == master.AttackCores[master.activeAttackCore].anims[2].anim;
        a16 = master.skeletonAnimation.AnimationState.ToString()    == master.AttackCores[master.activeAttackCore].anims[3].anim;

        foreach (var item in master.AttackCores[master.activeAttackCore].anims)
        {
            if (master.skeletonAnimation.AnimationState.ToString() == item.anim)
            {
                item.animationTime = master.skeletonAnimation.AnimationState.GetCurrent(0).AnimationTime;
            }
            else
            {
                item.animationTime = 0;
            }
        }

        #region Facing
        if (!a12 && !a13 && !a14 && !a15 && !a16) RecalculateFacing();
        #endregion

        #region Run & Idle
        if (a1 || a2 || a3 || a4 || a5 || a6 || a10 || a11)
        {
            if (Mathf.Abs(master.inputs.myLeftStick.x) > 0.2f) SetNewState(States.run, 0);
            if (Mathf.Abs(master.inputs.myLeftStick.x) <= 0.2f) SetNewState(States.neutralIdle, 0);
        }
        #endregion

        #region Falls
        if (!master.grounded && !a7 && !a8) SetNewState(States.fall, 0);
        #endregion

        #region Jumps
        bool ct = master.onAirTime < master.coyoteTime && a9;
        if (master.grounded)
        {
            if (a1 || a2 || a3 || a4 || a5 || a6 || a11 || a10 || a11 || a12)
            {
                if (Mathf.Abs(master.inputs.myLeftStick.x) > 0.2f)
                {
                    if (master.inputs.cross_tick) SetNewState(States.jumpFw, 0);
                }
                else
                {
                    if (master.inputs.cross_tick) SetNewState(States.jumpUp, 0);
                }
                if (master.inputs.cross_tick) master.currentJump += 1;
            }
        }
        else
        {
            if (ct)
            {
                if (Mathf.Abs(master.inputs.myLeftStick.x) > 0.2f)
                {
                    if (master.inputs.cross_tick) SetNewState(States.jumpFw, 0);
                }
                else
                {
                    if (master.inputs.cross_tick) SetNewState(States.jumpUp, 0);
                }
                if (master.inputs.cross_tick) master.currentJump += 1;
            }
        }
        #endregion

        #region Lands
        if ( ( ( master.onAirTime > 0.05f && ( a7 || a8 ) ) || a9 ) && master.grounded ) SetNewState(States.land, 0);
        if ( ( ( master.onAirTime > 0.05f && ( a7 || a8 ) ) || a9 ) && master.grounded ) master.currentJump = 0;
        #endregion

        #region Dash
        if (a1 || a2 || a3 || a4 || a5 || a6 || a10 || a11 || a13 || a14 || a15 || a16)
        {
            if (master.inputs.circle_tick) SetNewState(States.dash, 0);
        }
        #endregion

        #region 4 Fombo mode
        if (a1 || a2 || a3 || a4 || a5 || a6 || a10 || a11)
        {
            if (master.inputs.triangle_tick) SetNewState(States.Combo1, 0);
        }
        if (a13)
        {
            if (master.inputs.triangle_tick) SetNewState(States.Combo2, 0);
        }
        if (a14)
        {
            if (master.inputs.triangle_tick) SetNewState(States.Combo3, 0);
        }
        if (a15)
        {
            if (master.inputs.triangle_tick) SetNewState(States.Combo4, 0);
        }
        #endregion
    }
}
