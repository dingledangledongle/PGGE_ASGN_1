using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFlag : MonoBehaviour
{
    //getting references to the player
    public Animator animator;
    public CarlWheezerPlayer player;

    //method is called from animation events
    void AnimationEndFlag()
    {
        if (CheckAnimation("Recharge"))
        {
            player.isRecharging = false;
        }

        if (CheckAnimation("Attack"+player.currentAttackSeq))
        {
            player.canAttack = true;
        }

        if (CheckAnimation("Emote" + player.currentEmote))
        {
            player.endEmote = true;
        }

        if (CheckAnimation("Jump"))
        {
            //player.mPlayerMovement.Jump();
        }
    }

    bool CheckAnimation(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
