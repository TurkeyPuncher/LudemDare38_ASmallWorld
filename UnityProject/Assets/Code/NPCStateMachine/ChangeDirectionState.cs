using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirectionState : BaseState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        m_availableTriggers = new string[]{
            "Idle",
            "Walk" };
        
        m_npc.ChangeDirection(m_minStateInSeconds);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_npc.Stop();
    }
}
