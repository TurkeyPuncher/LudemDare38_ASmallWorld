using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCurveScale : MonoBehaviour
{
    [SerializeField]
    private bool m_doOnEnable = true;

    [SerializeField]
    private Vector3 m_relativeScale = Vector3.one;

    [SerializeField]
    public float m_duration = 2f;

    [SerializeField]
    private AnimationCurve m_animCurve = null;

    private Vector3 m_original;

    void Awake()
    {
        m_original = transform.localScale;
    }

    void OnEnable()
    {
        if (!m_doOnEnable)
            return;
        DoAnim();
    }

    public IEnumerator DoAnimRoutine()
    {
        float deltaTime = 0f;
        while(deltaTime < m_duration)
        {
            deltaTime += Time.deltaTime;
            var delta = m_relativeScale * m_animCurve.Evaluate(deltaTime/m_duration);
            transform.localScale = m_original + delta;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = m_original + (m_relativeScale * m_animCurve.Evaluate(1f));
    }

    public void DoAnim()
    {
        StopAllCoroutines();
        StartCoroutine(DoAnimRoutine());
    }
}
