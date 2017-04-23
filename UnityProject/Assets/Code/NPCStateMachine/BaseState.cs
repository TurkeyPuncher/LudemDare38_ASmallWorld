﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : StateMachineBehaviour
{
    [SerializeField]
    protected float m_minStateInSeconds = 1f;
    [SerializeField]
    protected float m_maxStateInSeconds = 2f;

    protected NPC m_npc;
    protected float m_stateInSeconds;
    protected string[] m_availableTriggers = {};

    // This is to ignore the update when we want to just wait for exit
    protected bool m_ignoreUpdateAndWaitForExit = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_npc = animator.GetComponent<NPC>();
        m_stateInSeconds = Time.time + Random.Range(m_minStateInSeconds, m_maxStateInSeconds);
        m_ignoreUpdateAndWaitForExit = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_ignoreUpdateAndWaitForExit)
            return;
        
        if (Time.time > m_stateInSeconds)
        {
            // Change State
            var trigger = m_availableTriggers[Random.Range(0, m_availableTriggers.Length)];
            animator.SetTrigger(trigger);
            m_ignoreUpdateAndWaitForExit = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
