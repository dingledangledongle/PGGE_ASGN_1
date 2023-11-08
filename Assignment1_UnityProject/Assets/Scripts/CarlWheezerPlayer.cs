using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;
using PGGE;

public class CarlWheezerPlayer: Player
{
    bool[] mFiring = new bool[3];

    // Start is called before the first frame update
    void Start()
    {
        mFsm.Add(new PlayerState_MOVEMENT(this));
        mFsm.Add(new PlayerState_ATTACK(this));
        mFsm.Add(new PlayerState_RELOAD(this));
        mFsm.SetCurrentState((int)PlayerStateType.MOVEMENT);

        PlayerConstants.PlayerMask = mPlayerMask;
    }

    void Update()
    {
        mFsm.Update();
        Aim();

        // For Student ----------------------------------------------------//
        // Implement the logic of button clicks for shooting. 
        //-----------------------------------------------------------------//

        if (Input.GetButton("Fire1"))
        {
            mAttackButtons[0] = true;
            mAttackButtons[1] = false;
            mAttackButtons[2] = false;
        }
        else
        {
            mAttackButtons[0] = false;
        }

        if (Input.GetButton("Fire2"))
        {
            mAttackButtons[0] = false;
            mAttackButtons[1] = true;
            mAttackButtons[2] = false;
        }
        else
        {
            mAttackButtons[1] = false;
        }

        if (Input.GetButton("Fire3"))
        {
            mAttackButtons[0] = false;
            mAttackButtons[1] = false;
            mAttackButtons[2] = true;
        }
        else
        {
            mAttackButtons[2] = false;
        }

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
}
