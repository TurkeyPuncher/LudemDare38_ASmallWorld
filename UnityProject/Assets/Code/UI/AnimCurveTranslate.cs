using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCurveTranslate : MonoBehaviour
{
    [SerializeField]
    private bool m_doOnEnable = true;

    [SerializeField]
    private Vector3 m_relativeTranslate = Vector3.zero;

    [SerializeField]
    private float m_duration = 2f;

    [SerializeField]
    private AnimationCurve m_animCurve = null;

    private Vector3 m_original;

    void Awake()
    {
        m_original = transform.localPosition;
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
        while (deltaTime < m_duration)
        {
            deltaTime += Time.deltaTime;
            var delta = m_relativeTranslate * m_animCurve.Evaluate(deltaTime / m_duration);
            transform.localPosition = m_original + delta;
            yield return new WaitForEndOfFrame();
        }
        
        transform.localScale = m_relativeTranslate * m_animCurve.Evaluate(1f);
    }

    public void DoAnim()
    {
        StopAllCoroutines();
        StartCoroutine(DoAnimRoutine());
    }
}
