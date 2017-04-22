using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavigation : MonoBehaviour
{
    // Rigidbody
    [SerializeField]
    float m_speedScaleX = 60;
    [SerializeField]
    float m_speedScaleZ = 100;
    
    // Finger touch
    Vector3 m_fingerPos = Vector3.zero;
    Vector3 m_fingerMovedBy = Vector3.zero;

    float m_slideMagnitudeX = 0.0f;
    float m_slideMagnitudeZ = 0.0f;

    void Update()
    {
        // Detect touch and move
        if (Input.GetMouseButtonDown(0))
        {
            m_fingerPos = Input.mousePosition;
            m_slideMagnitudeX = 0f;
            m_slideMagnitudeZ = 0f;
        }
        if (Input.GetMouseButton(0))
        {
            // The delta since last position.
            m_fingerMovedBy = Input.mousePosition - m_fingerPos;

            // slide horizontal
            m_slideMagnitudeX = m_fingerMovedBy.x / Screen.height;

            // slide vertical
            m_slideMagnitudeZ = m_fingerMovedBy.y / Screen.width;

            m_fingerPos = Input.mousePosition;
        }

        // movement
        float rotationX = m_slideMagnitudeX * m_speedScaleX;
        float rotationY = m_slideMagnitudeZ * m_speedScaleZ;
        transform.RotateAround(Vector3.zero, Camera.main.transform.up, rotationX);
        transform.RotateAround(Vector3.zero, Camera.main.transform.right, -rotationY);
    }
}

