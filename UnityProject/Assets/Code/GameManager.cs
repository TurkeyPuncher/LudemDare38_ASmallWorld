using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Camera m_mainCamera = null;
    
    private static GameManager m_instance;
    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject newObj = new GameObject("GameManager");
                m_instance = newObj.AddComponent<GameManager>();
            }
            return m_instance;
        }
    }

    public Camera MainCamera { get; private set; }

    void Awake()
    {
        // Instance
        if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;

        // TODO Remove
        for (int i = 0; i < 20; i++)
        {
            var spherePoint = Random.insideUnitSphere * 5;
            NPCFactory.Instance.CreateNPC(spherePoint);
        }

        if (MainCamera == null)
            MainCamera = Camera.main;
    }
}
