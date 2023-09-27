using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    [HideInInspector] public SkeletonAnimation skeletonAnimation;
    [HideInInspector] public SkinManager skinManager;
    [HideInInspector] public CharacterAnimator animator;
    [HideInInspector] public CharacterMotions motions;
    [HideInInspector] public Inputs inputs;
    [HideInInspector] public CameraShaker camShaker;

    [HideInInspector] public Rigidbody2D body;
    [HideInInspector] public Collider2D pushbox;
    [HideInInspector] public float onAirTime;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool facingRight = true;
    public LayerMask grounderIgnore;
    [Header("Motions")]
    public byte moveSpeed;
    public AnimationCurve dashSpeed;
    public byte jumpSpeed;
    public float coyoteTime;
    public byte normalGravity;
    public byte shortGravity;
    public byte maxFallSpeed;
    public byte maxJumps;
    public byte currentJump;
    [Range(1, 2)] public float airMovilityMultiply;
    [Header("Cores")]
    public int activeDefenseCore;
    public int activeFunctionCore;
    public int activeAttackCore;
    public CoreContainer[] DefenseCores;
    public CoreContainer[] FunctionCores;
    public CoreContainer[] AttackCores;

    bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(pushbox.bounds.center, pushbox.bounds.size, 0, Vector2.down, 0.3f, ~grounderIgnore);

        bool b = hit != false ? hit.collider.CompareTag("Landable") : false;

        return b;
    }

    void HandleAnimationStateEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        //Jump
        if (motions.jumpEventData == e.Data) motions.MasterJump();
        //Attack Cores
<<<<<<< HEAD
        if (animator.cameraShakeEventData == e.Data) camShaker.Shake(2, 0.1f);
        //Orientate
        if (animator.orientateEventData == e.Data) animator.RecalculateFacing();
=======
        foreach (var item in AttackCores)
        {
            foreach (var item2 in item.anims)
            {
                item2.event1Triggered = item2.event1Data == e.Data;
            }
        }
        foreach (var item in AttackCores[activeAttackCore].anims)
        {
            if (item.event1Triggered)
            {
                camShaker.Shake(4, 0.1f);
            }
        }
>>>>>>> parent of e583162 (Update)
    }

    private void Start()
    {
        camShaker = FindObjectOfType<CameraShaker>();
        inputs = FindObjectOfType<Inputs>();
        skinManager = GetComponent<SkinManager>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        animator = GetComponent<CharacterAnimator>();
        motions = GetComponent<CharacterMotions>();
        body = GetComponent<Rigidbody2D>();
        pushbox = GetComponent<Collider2D>();

        animator.master = this;
        motions.master = this;
        skinManager.master = this;

        motions.jumpEventData = skeletonAnimation.Skeleton.Data.FindEvent(motions.jumpEvent);

        animator.cameraShakeEventData = skeletonAnimation.Skeleton.Data.FindEvent(animator.cameraShakeEvent);

        animator.orientateEventData = skeletonAnimation.Skeleton.Data.FindEvent(animator.orientateEvent);

        skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
    }

    void Update()
    {
        skeletonAnimation.skeleton.ScaleX = facingRight ? 1 : -1;
        onAirTime = onAirTime >= 225 ? 225 : grounded ? onAirTime : onAirTime += 1 * Time.deltaTime;
        if (grounded) onAirTime = 0;

        grounded = CheckGround();
        skinManager.SetSkin();
        animator.Animate();
    }

    void FixedUpdate()
    {
        motions.MasterMoveCharacter();
        motions.MasterHandleGravity();
    }
}
[System.Serializable]
public struct CoreContainer
{
    public string coreName;
    public bool Owned;
    [SpineSkin] public string skinToUse;
    public CoreAnims[] anims;
}
[System.Serializable]
public class CoreAnims
{
    [SpineAnimation] public string anim;
    public float animationTime;
<<<<<<< HEAD
    public AnimationCurve speedOverTime;
=======
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string event1;
    public Spine.EventData event1Data;
    public bool event1Triggered;
>>>>>>> parent of e583162 (Update)
}
