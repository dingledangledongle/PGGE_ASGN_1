using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;
using PGGE;

public class CarlWheezerPlayer : MonoBehaviour
{
    public FSM mFsm = new FSM();
    public Animator mAnimator;
    public PlayerMovement mPlayerMovement;

    public LayerMask mPlayerMask;
    public AudioSource mAudioSource;

    public int currentAttackSeq;

    public bool isAttacking;
    public bool canAttack;
    public bool isRecharging;
    public bool isEmoting;
   

    void Start()
    {
        mFsm.Add(new CWState_MOVEMENT(this));
        mFsm.Add(new CWState_ATTACK(this));
        mFsm.Add(new CWState_RECHARGE(this));
        mFsm.SetCurrentState((int)CWStateType.MOVEMENT);

        PlayerConstants.PlayerMask = mPlayerMask;
    }

    void Update()
    {
        mFsm.Update();

        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
            canAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            mFsm.SetCurrentState((int)CWStateType.RECHARGE);
        }

        //emotes
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mAnimator.SetTrigger("Emote1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mAnimator.SetTrigger("Emote2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mAnimator.SetTrigger("Emote3");
        }
    }

    public void Move()
    {
        mPlayerMovement.HandleInputs();
        mPlayerMovement.Move();
    }
}
