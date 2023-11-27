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

    public bool isAttacking;

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
        }
        


        //emotes
        if (Input.GetKey(KeyCode.Alpha1))
        {
            mAnimator.SetTrigger("Emote1");
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            mAnimator.SetTrigger("Emote2");
        }

        if (Input.GetKey(KeyCode.Alpha3))
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
