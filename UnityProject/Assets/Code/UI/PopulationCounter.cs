using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationCounter : MonoBehaviour
{
    [SerializeField]
    Text m_text = null;

    [SerializeField]
    AnimCurveScale m_curveScale = null;

    void Awake()
    {
        m_text = GetComponent<Text>();
        m_curveScale = GetComponent<AnimCurveScale>();
    }

    public void SetPopulation(float value)
    {
        m_text.text = value.ToString();
        m_curveScale.DoAnim();
    }
}
