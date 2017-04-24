using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Camera m_mainCamera = null;

    [SerializeField]
    private int m_startPopulation = 10;

    [SerializeField]
    private PopulationCounter m_populationCounter = null;
    
    
    // Behaviour
    [SerializeField]
    List<LoveBehaviourTrait> m_loveBehaviours = new List<LoveBehaviourTrait>();

    [SerializeField]
    List<HateBehaviourTrait> m_hateBehaviours = new List<HateBehaviourTrait>();

    [SerializeField]
    private LoveHateMessages m_loveHateMessages = null;

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

    public Camera MainCamera { get { return m_mainCamera; } }
    public int Population { get; private set; }

    void Awake()
    {
        // Instance
        if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;

        if (m_mainCamera == null)
            m_mainCamera = Camera.main;

        StartCoroutine(StartRoutine(m_startPopulation, 0f));
    }

    void Update()
    {
        // Since we have no player controller, placing here 
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator StartRoutine(int amount, float timeInSeconds)
    {
        // TODO Remove
        for (int i = 0; i < amount; i++)
        {
            var spherePoint = Random.insideUnitSphere * 5;
            NPCFactory.Instance.CreateNPC(spherePoint);
            yield return new WaitForSeconds(timeInSeconds);
        }

        // Create starting behaviours
        GenerateBehaviors();
        yield return new WaitForSeconds(1f);
        // Create starting behaviours
        GenerateBehaviors();
        yield return new WaitForSeconds(1f);
        // Create starting behaviours
        GenerateBehaviors();
        yield return new WaitForSeconds(1f);
        // Create starting behaviours
        GenerateBehaviors();
    }

    void GenerateBehaviors()
    {
        var love = new LoveBehaviourTrait();
        var hate = new HateBehaviourTrait();
        m_loveBehaviours.Add(love);
        m_hateBehaviours.Add(hate);

        m_loveHateMessages.AddLove(love.TraitMessage);
        m_loveHateMessages.AddHate(hate.TraitMessage);

        var npcs = NPCFactory.Instance.GetComponentsInChildren<NPC>();
        foreach (var n in npcs)
        {
            n.AddLove(love);
            n.AddHate(hate);
        }
    }

    public void AddPopulation(NPC npc)
    {
        foreach (var love in m_loveBehaviours)
            npc.AddLove(love);
        foreach (var hate in m_hateBehaviours)
            npc.AddLove(hate);

        Population++;
        m_populationCounter.SetPopulation(Population);
    }

    public void RemovePopulation()
    {
        Population--;
        m_populationCounter.SetPopulation(Population);
    }
}
