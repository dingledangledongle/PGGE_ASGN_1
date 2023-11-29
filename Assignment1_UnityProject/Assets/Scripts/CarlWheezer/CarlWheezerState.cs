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
        base.Enter();
    }

    public override void Update()
    {
        mPlayer.Move();

        //enter attack state
        if (mPlayer.isAttacking)
        {
            mPlayer.mFsm.SetCurrentState((int)CWStateType.ATTACK);
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

    public CWState_ATTACK(CarlWheezerPlayer player) : base(player)
    {
        mId = (int)(CWStateType.ATTACK);
    }

    public override void Enter()
    {
        timer = 0;
        attackSeq = 0;
        Debug.Log("enter attack");
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        if (timer > 3)
        {
            //exit the attack
            mPlayer.mFsm.SetCurrentState((int)CWStateType.MOVEMENT);
        }

        if (attackSeq == 3)
        {
            //restart sequence
            attackSeq = 0;
        }

        //mPlayer.currentAttackSeq = attackSeq;

        if (mPlayer.isAttacking && mPlayer.canAttack)
        {
            Debug.Log(attackSeq);
            attackName = "Attack" + attackSeq;
            mPlayer.mAnimator.SetTrigger(attackName);
            mPlayer.canAttack = false;
            attackSeq++;
            timer = 0;
            mPlayer.isAttacking = false;
        }


        timer += Time.fixedDeltaTime;
        base.Update();
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
        Debug.Log("enter recharge");
        base.Enter();
    }

    public override void Update()
    {
        if (!mPlayer.isRecharging)
        {
            mPlayer.mAnimator.SetBool("Recharge", false);
            mFsm.SetCurrentState((int)CWStateType.MOVEMENT);
        }
        //base.Update();
    }

}

public class CWState_EMOTE : CarlWheezerState
{
    public CWState_EMOTE(CarlWheezerPlayer player) : base(player)
    {
        mId = (int)(CWStateType.EMOTE);
    }

    public override void Enter()
    {

        base.Enter();
    }
}