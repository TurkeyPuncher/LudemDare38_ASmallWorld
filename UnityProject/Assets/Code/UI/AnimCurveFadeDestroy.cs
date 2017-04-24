using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AnimCurveFadeDestroy : MonoBehaviour
{
    [SerializeField]
    private bool m_doOnEnable = true;

    [SerializeField]
    private bool m_destroyOnEnd = true;

    [SerializeField]
    private float m_duration = 1f;

    [SerializeField]
    private AnimationCurve m_animCurve = null;

    CanvasGroup m_canvasGroup = null;

    void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
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
            m_canvasGroup.alpha = m_animCurve.Evaluate(deltaTime / m_duration);
            yield return new WaitForEndOfFrame();
        }
        m_canvasGroup.alpha = m_animCurve.Evaluate(1f);
        if (m_destroyOnEnd)
            DestroyImmediate(gameObject);
    }

    public void DoAnim()
    {
        StopAllCoroutines();
        StartCoroutine(DoAnimRoutine());
    }
}
