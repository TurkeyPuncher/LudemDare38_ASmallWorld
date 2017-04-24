using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavigation : MonoBehaviour
{
    [SerializeField]
    float m_speedScaleX = 60;

    [SerializeField]
    float m_speedScaleY = 100;

    [SerializeField]
    float m_dampen = 0.9f;

    [SerializeField]
    Transform m_cameraZoomTransform = null;

    [SerializeField]
    AnimationCurve m_cameraZoomCurve = null;
    [SerializeField]
    AnimationCurve m_cameraZoomRotateCurve = null;
    [SerializeField]
    float m_cameraZoomRate = -0.01f;
    [SerializeField]
    float m_cameraZoomDelta = 0.5f;

    [SerializeField]
    float m_cameraZoomRotateFrom = -30f;
    [SerializeField]
    float m_cameraZoomRotateTo = 0;

    [SerializeField]
    float m_cameraZoomTranslateFrom = 0f;
    [SerializeField]
    float m_cameraZoomTranslateTo = -10f;

    [SerializeField]
    float m_cameraFOVFrom = 40f;
    [SerializeField]
    float m_cameraFOVTo = 25f;

    // Finger touch
    Vector3 m_fingerPos = Vector3.zero;
    Vector3 m_fingerMovedBy = Vector3.zero;

    float m_slideMagnitudeX = 0.0f;
    float m_slideMagnitudeY = 0.0f;

    // movement
    float m_rotationX = 0f;
    float m_rotationY = 0f;
    float m_rotationZ = 0f;
    Camera m_mainCamera;

    void Start()
    {
        m_mainCamera = GameManager.Instance.MainCamera;
        CameraZoom();
    }

    void Update()
    {
        // Detect touch and move
        if (Input.GetMouseButtonDown(0))
        {
            m_fingerPos = Input.mousePosition;
            m_slideMagnitudeX = 0f;
            m_slideMagnitudeY = 0f;
        }

        // Calculate the movement by mouse drag
        if (Input.GetMouseButton(0))
        {
            // The delta since last position.
            m_fingerMovedBy = Input.mousePosition - m_fingerPos;

            // slide horizontal
            m_slideMagnitudeX = m_fingerMovedBy.x / Screen.height;
            m_rotationX = m_slideMagnitudeX * m_speedScaleX;

            // slide vertical
            m_slideMagnitudeY = m_fingerMovedBy.y / Screen.width;
            m_rotationY = m_slideMagnitudeY * m_speedScaleY;

            m_fingerPos = Input.mousePosition;
        }

        // AD keys strafe
        float horizontalMovement = Input.GetAxis("Horizontal");
        if (horizontalMovement != 0)
        {
            m_rotationZ = horizontalMovement;
        }

        // WS keys Zoom in and out
        float verticalMovement = Input.GetAxis("Vertical");
        if (verticalMovement != 0)
        {
            m_cameraZoomDelta = Mathf.Clamp(m_cameraZoomDelta + verticalMovement * m_cameraZoomRate, 0f, 1f);
            CameraZoom();
        }

        transform.RotateAround(Vector3.zero, m_mainCamera.transform.up, m_rotationX);
        transform.RotateAround(Vector3.zero, m_mainCamera.transform.right, -m_rotationY);
        transform.RotateAround(Vector3.zero, m_mainCamera.transform.forward, -m_rotationZ);

        // Dampeners
        m_rotationX *= m_dampen;
        m_rotationY *= m_dampen;
        m_rotationZ *= m_dampen;
    }

    void CameraZoom()
    {
        m_cameraZoomTransform.localEulerAngles = new Vector3(Mathf.Lerp(m_cameraZoomRotateFrom, m_cameraZoomRotateTo, m_cameraZoomRotateCurve.Evaluate(m_cameraZoomDelta)), 0f, 0f);
        m_cameraZoomTransform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(m_cameraZoomTranslateFrom, m_cameraZoomTranslateTo, m_cameraZoomCurve.Evaluate(m_cameraZoomDelta)));
        m_mainCamera.fieldOfView = Mathf.Lerp(m_cameraFOVFrom, m_cameraFOVTo, m_cameraZoomCurve.Evaluate(m_cameraZoomDelta));
    }
}

