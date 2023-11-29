using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;

public enum CWStateType
{
    MOVEMENT = 0,
    ATTACK,
    RECHARGE,
    EMOTE
}

public class CarlWheezerState : FSMState
{
    protected CarlWheezerPlayer mPlayer = null;

    public CarlWheezerState(CarlWheezerPlayer player)
         : base()
    {
        mPlayer = player;
        mFsm = mPlayer.mFsm;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

public class CWState_MOVEMENT : CarlWheezerState
{
    public CWState_MOVEMENT(CarlWheezerPlayer player) : base(player)
    {
        mId = (int)(CWStateType.MOVEMENT);
    }

    public override void Enter()
    {
        Debug.Log("enter movement");
        base.Enter();
    }

    public override void Update()
    {
        mPlayer.Move();

        //enter attack state
        if (Input.GetButtonDown("Fire1"))
        {
            mPlayer.canAttack = true;
            mFsm.SetCurrentState((int)CWStateType.ATTACK);
        }

        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

public class CWState_ATTACK : CarlWheezerState
{
    string attackName;
    int attackSeq;
    float timer;
    bool desireAttack;

    public CWState_ATTACK(CarlWheezerPlayer player) : base(player)
    {
        mId = (int)(CWStateType.ATTACK);
    }

    public override void Enter()
    {
        timer = 0;
        attackSeq = 0;
        mPlayer.currentAttackSeq = attackSeq;
        desireAttack = true;
        base.Enter();
    }

    public override void Update()
    {
        //timer to exit the attack if nothing is done
        if (timer > 1.5)
        {
            //exit the attack
            mPlayer.mFsm.SetCurrentState((int)CWStateType.MOVEMENT);
        }

        //check if it is last in the sequence
        if (attackSeq == 3)
        {
            //restart sequence
            attackSeq = 0;
        }

        if (Input.GetButtonDown("Fire1"))
            desireAttack = true;

        //attacking logic
        if (desireAttack && mPlayer.canAttack)
        {
            attackName = "Attack" + attackSeq;
            mPlayer.currentAttackSeq = attackSeq;
            mPlayer.mAnimator.SetTrigger(attackName);
            mPlayer.canAttack = false;
            desireAttack = false;
            attackSeq++;
            timer = 0;
        }

        base.Update();
    }

    public override void FixedUpdate()
    {
        //increment the timer
        timer += Time.fixedDeltaTime;
        base.FixedUpdate();
    }
}

public class CWState_RECHARGE : CarlWheezerState
{
    public CWState_RECHARGE(CarlWheezerPlayer player) : base(player)
    {
        mId = (int)(CWStateType.RECHARGE);
    }
    
    public override void Enter()
    {
        mPlayer.mAnimator.SetBool("Recharge", true);
        mPlayer.isRecharging = true;
        base.Enter();
    }

    public override void Update()
    {
        //check if the player is still recharging
        if (!mPlayer.isRecharging)
        {
            mPlayer.mAnimator.SetBool("Recharge", false);
            mFsm.SetCurrentState((int)CWStateType.MOVEMENT);
        }

        //check if the player wants to attack and switches to attack state
        if (Input.GetButtonDown("Fire1"))
        {
            mPlayer.canAttack = true;
            mFsm.SetCurrentState((int)CWStateType.ATTACK);
        }
    }

}

public class CWState_EMOTE : CarlWheezerState
{
    public CWState_EMOTE(CarlWheezerPlayer player) : base(player)
    {
        mId = (int)(CWStateType.EMOTE);
    }

    int emoteNumber;

    public override void Enter()
    {
        //check which button is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mPlayer.mAnimator.SetBool("Emote1",true);
            emoteNumber = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mPlayer.mAnimator.SetBool("Emote2",true);
            emoteNumber = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mPlayer.mAnimator.SetBool("Emote3",true);
            emoteNumber = 3;
        }

        mPlayer.endEmote = false;
        mPlayer.currentEmote = emoteNumber;
        base.Enter();
    }

    public override void Update()
    {
        //cancel emote if player wants to attack
        if (Input.GetButtonDown("Fire1"))
        {
            mPlayer.canAttack = true;
            mFsm.SetCurrentState((int)CWStateType.ATTACK);
        }

        //cancel emote if player wants to move
        if (mPlayer.mPlayerMovement.isMoving) 
        {
            mFsm.SetCurrentState((int)CWStateType.MOVEMENT);
        }

        //switch to movement state once it ends
        if (mPlayer.endEmote)
        {
            mFsm.SetCurrentState((int)CWStateType.MOVEMENT);
        }

        mPlayer.mPlayerMovement.HandleInputs();
        base.Update();
    }

    public override void Exit()
    {
        string name = "Emote" + emoteNumber;

        mPlayer.mAnimator.SetBool(name, false);
        mPlayer.isEmoting = false;
        base.Exit();
    }
}