using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavigation : MonoBehaviour
{
    [SerializeField]
    float m_speedScaleX = 60;
    [SerializeField]
    float m_speedScaleY = 100;
    //[SerializeField]
    //float m_speedScaleZ = 60;
    [SerializeField]
    float m_dampen = 0.9f;

    // Finger touch
    Vector3 m_fingerPos = Vector3.zero;
    Vector3 m_fingerMovedBy = Vector3.zero;

    float m_slideMagnitudeX = 0.0f;
    float m_slideMagnitudeY = 0.0f;

    // movement
    float m_rotationX = 0f; 
    float m_rotationY = 0f;
    float m_rotationZ = 0f;

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
            m_rotationX = m_slideMagnitudeX* m_speedScaleX;

            // slide vertical
            m_slideMagnitudeY = m_fingerMovedBy.y / Screen.width;
            m_rotationY = m_slideMagnitudeY* m_speedScaleY;

            m_fingerPos = Input.mousePosition;
        }

        // Calculate movement using asdw keys
        float horizontalMovement = Input.GetAxis("Horizontal");
        if (horizontalMovement != 0)
        {
            m_rotationZ = horizontalMovement;
        }

        float verticalMovement = Input.GetAxis("Vertical");
        if (verticalMovement != 0)
        {
            m_rotationY = -verticalMovement;
        }


        transform.RotateAround(Vector3.zero, Camera.main.transform.up, m_rotationX);
        transform.RotateAround(Vector3.zero, Camera.main.transform.right, -m_rotationY);
        transform.RotateAround(Vector3.zero, Camera.main.transform.forward, -m_rotationZ);

        // Dampeners
        m_rotationX *= m_dampen;
        m_rotationY *= m_dampen;
        m_rotationZ *= m_dampen;
    }
}

