using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveState : BaseState
{
    // Called by NPC when collision occurs
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        m_availableTriggers = new string[]{
            "ChangeDirection",  "Idle", "Walk" };

        m_npc.MakeLove(m_stateInSeconds);
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
            ExitCallback();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void ExitCallback()
    {
        m_npc.Stop();
    }
}
