using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationCounter : MonoBehaviour
{
    [SerializeField]
    Text m_text = null;

    [SerializeField]
    BounceScale m_bounceScale = null;

    void Awake()
    {
        m_text = GetComponent<Text>();
        m_bounceScale = GetComponent<BounceScale>();
    }

    public void SetPopulation(float value)
    {
        m_text.text = value.ToString();
        m_bounceScale.Bounce();
    }
}
