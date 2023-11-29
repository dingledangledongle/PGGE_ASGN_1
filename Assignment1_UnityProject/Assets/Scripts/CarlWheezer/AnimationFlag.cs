using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFlag : MonoBehaviour
{
    public Animator animator;
    public CarlWheezerPlayer player;
    void AnimationEndFlag()
    {
        Debug.Log("animation flag");
        if (CheckAnimation("Recharge"))
        {
            player.isRecharging = false;
        }

        if (CheckAnimation("Attack"+player.currentAttackSeq))
        {
            Debug.Log($"Attack{player.currentAttackSeq} Flag");
            player.canAttack = true;
        }
    }

    bool CheckAnimation(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
