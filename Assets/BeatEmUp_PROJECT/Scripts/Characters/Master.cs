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
    public CoreContainer[] DefenseCores;
    public int activeFunctionCore;
    public CoreContainer[] FunctionCores;
    public int activeAttackCore;
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
        bool jumpEventChecked = (motions.jumpEventData == e.Data);
        if (jumpEventChecked) motions.MasterJump();
    }

    private void Start()
    {
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
        skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
    }

    void Update()
    {
        skeletonAnimation.skeleton.ScaleX = facingRight ? 1 : -1;
        onAirTime = onAirTime >= 225 ? 225 : grounded ? onAirTime : onAirTime += 1 * Time.deltaTime;

        grounded = CheckGround();
        skinManager.SetSkin();
        animator.Animate();
    }

    void FixedUpdate()
    {
        motions.MasterMoveCharacter();
        motions.MasterHandleGravity();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(pushbox.bounds.center, pushbox.bounds.size);
    }
}
[System.Serializable]
public struct CoreContainer
{
    public bool Owned;
    [SpineSkin] public string skinToUse;
}
