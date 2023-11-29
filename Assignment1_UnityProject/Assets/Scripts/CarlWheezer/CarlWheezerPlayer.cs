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
    public int currentEmote;

    public bool canAttack;
    public bool isRecharging;
    public bool isEmoting;
    public bool endEmote;

    void Start()
    {
        //adding all the states
        mFsm.Add(new CWState_MOVEMENT(this));
        mFsm.Add(new CWState_ATTACK(this));
        mFsm.Add(new CWState_RECHARGE(this));
        mFsm.Add(new CWState_EMOTE(this));

        //set the starting state to be movement
        mFsm.SetCurrentState((int)CWStateType.MOVEMENT);

        PlayerConstants.PlayerMask = mPlayerMask;
    }

    void Update()
    {
        mFsm.Update();
        

        //go into recharge state
        if (Input.GetKeyDown(KeyCode.R))
        {
            mFsm.SetCurrentState((int)CWStateType.RECHARGE);
        }

        //go into emote state when any of this buttons are pressed and the player is not emoting
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)) && !isEmoting)
        {
            mFsm.SetCurrentState((int)CWStateType.EMOTE);
            isEmoting = true;
        }
    }

    private void FixedUpdate()
    {
        mFsm.FixedUpdate();
    }

    public void Move()
    {
        mPlayerMovement.HandleInputs();
        mPlayerMovement.Move();
    }
}
