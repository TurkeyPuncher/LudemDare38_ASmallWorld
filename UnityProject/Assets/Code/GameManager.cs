using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Camera m_mainCamera = null;

    [SerializeField]
    private PopulationCounter m_populationCounter = null;

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
    public int Population{ get; private set; }
    
    void Awake()
    {
        // Instance
        if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
        
        if (MainCamera == null)
            MainCamera = Camera.main;

        StartCoroutine(TestAdd());
    }

    void Update()
    { 
        // Since we have no player controller, placing here 
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator TestAdd()
    {
        // TODO Remove
        for (int i = 0; i < 200; i++)
        {
            var spherePoint = Random.insideUnitSphere * 5;
            NPCFactory.Instance.CreateNPC(spherePoint);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void AddPopulation()
    {
        Population++;
        m_populationCounter.SetPopulation(Population);
    }

    public void RemovePopulation()
    {
        Population--;
        m_populationCounter.SetPopulation(Population);
    }

    
}
