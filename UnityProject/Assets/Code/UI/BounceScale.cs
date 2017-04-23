using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScale : MonoBehaviour
{
    [SerializeField]
    private bool m_bounceOnEnable = true;

    [SerializeField]
    private float m_scaleAmount = 2f;

    [SerializeField]
    private float m_scaleTime = 2f;

    [SerializeField]
    private AnimationCurve m_bounceCurve = null;

    private Vector3 m_originalScale;

    void Awake()
    {
        m_originalScale = transform.localScale;
    }

    void OnEnable()
    {
        if (!m_bounceOnEnable)
            return;
        Bounce();
    }

    public IEnumerator BounceRoutine()
    {
        float deltaTime = 0f;
        while(deltaTime < m_scaleTime)
        {
            deltaTime += Time.deltaTime;
            float animScalar = m_scaleAmount * m_bounceCurve.Evaluate(deltaTime/m_scaleTime);
            transform.localScale = m_originalScale + (Vector3.one * animScalar);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = m_originalScale;
    }

    public void Bounce()
    {
        StopAllCoroutines();
        StartCoroutine(BounceRoutine());
    }
}
