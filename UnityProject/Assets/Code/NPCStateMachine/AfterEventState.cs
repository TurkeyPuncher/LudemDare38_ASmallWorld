﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterEventState : BaseState
{
    // Called by NPC when collision occurs
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        m_availableTriggers = new string[]{
            "Walk", "Idle", "ChangeDirection" };
    }
    
}
