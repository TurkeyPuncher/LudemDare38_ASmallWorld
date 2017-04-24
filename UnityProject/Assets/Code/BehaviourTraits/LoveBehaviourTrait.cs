using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LoveBehaviourTrait : BehaviourTrait
{
    public LoveBehaviourTrait()
    {
        // Generate 
        m_sourceTrigger = RandomTrigger();
        m_targetTrigger = RandomTrigger();
        GenerateMessage();
    }

    // Generates the trait description
    override public void GenerateMessage()
    {
        var sourceString = m_sourceTrigger.ToString().Replace("_", " ");
        var targetString = m_targetTrigger.ToString().Replace("_", " ");
        m_traitMessage = string.Format("{0} Loves {1}", sourceString, targetString);
    }
}
