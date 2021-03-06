﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera m_mainCamera;

    void Start()
    {
        m_mainCamera = GameManager.Instance.MainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_mainCamera.transform, transform.parent.up);
    }
}
