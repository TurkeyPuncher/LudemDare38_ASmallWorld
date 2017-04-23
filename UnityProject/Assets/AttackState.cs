﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        m_availableTriggers = new string[]{
            "Walk",
            "ChangeDirection" };

        m_npc.Attack();
    }
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void ExitCallback()
    {
        m_npc.Stop();
    }
}
