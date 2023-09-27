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
        if (body.velocity.y > 0 && onAirTime > 0) return false;

        RaycastHit2D hit = Physics2D.BoxCast(pushbox.bounds.center + new Vector3(0, -4), new Vector2(pushbox.bounds.size.x, 2), 0, Vector2.down, 0.5f, ~grounderIgnore);

        bool b = hit != false ? hit.collider.CompareTag("Landable") : false;

        return b;
    }

    void HandleAnimationStateEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        //Jump
        if (motions.jumpEventData == e.Data) motions.MasterJump();
        //Attack Cores
        if (animator.cameraShakeEventData == e.Data) camShaker.Shake(2, 0.1f);
        //Orientate
        if (animator.orientateEventData == e.Data) animator.RecalculateFacing();
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
    public AnimationCurve speedOverTime;
}
