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
    private int m_breedMin = 2;

    [SerializeField]
    private int m_breedMax = 10;

    [SerializeField]
    private float m_breedRate = 0.5f;

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
        //GenerateBehaviors();
        yield return new WaitForSeconds(1f);
        // Create starting behaviours
        GenerateTestBehaviors();
        yield return new WaitForSeconds(1f);
        // Create starting behaviours
    }

    void GenerateTestBehaviors()
    {
        var love = new LoveBehaviourTrait();
        love.Source = BehaviourTrait.Trigger.Blue_FaceColor;
        love.Target = BehaviourTrait.Trigger.Green_FaceColor;
        m_loveBehaviours.Add(love);

        m_loveHateMessages.AddLove(love.TraitMessage);

        var npcs = NPCFactory.Instance.GetComponentsInChildren<NPC>();
        foreach (var n in npcs)
        {
            n.AddLove(love);
        }
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

    IEnumerator BreedRoutine(NPC source, NPC target, int count)
    {
        for (int i = 0; i < count; i++)
        {
            NPCFactory.Face face = (Random.Range(0f, 1f) > 0.5f) ? source.FaceType : target.FaceType;
            NPCFactory.FaceColor faceColor = (Random.Range(0f, 1f) > 0.5f) ? source.FaceColorType : target.FaceColorType;
            NPCFactory.Mouth mouth = (Random.Range(0f, 1f) > 0.5f) ? source.MouthType : target.MouthType;
            NPCFactory.Nose nose = (Random.Range(0f, 1f) > 0.5f) ? source.NoseType : target.NoseType;
            NPCFactory.Eyes eyes = (Random.Range(0f, 1f) > 0.5f) ? source.EyesType : target.EyesType;
            NPCFactory.Eyebrows eyebrows = (Random.Range(0f, 1f) > 0.5f) ? source.BrowType : target.BrowType;
            NPCFactory.Hair hair = (Random.Range(0f, 1f) > 0.5f) ? source.HairType : target.HairType;
            NPCFactory.HairColor hairColor = (Random.Range(0f, 1f) > 0.5f) ? source.HairColorType : target.HairColorType;
            NPCFactory.Ears ears = (Random.Range(0f, 1f) > 0.5f) ? source.EarType : target.EarType;
            bool hasGlasses = (Random.Range(0f, 1f) > 0.5f) ? source.HasGlasses : target.HasGlasses;
            bool isFemale = (Random.Range(0f, 1f) > 0.5f) ? false : true;

            var distanceVector = source.transform.position - target.transform.position;
            var pointBetween = source.transform.position - (distanceVector * 0.5f);
            var spawnLocation = Random.insideUnitSphere + pointBetween;
            NPCFactory.Instance.CreateNPC(spawnLocation,
                face,
                faceColor,
                mouth,
                nose,
                eyes,
                0,
                0,
                eyebrows,
                hair,
                0,
                hairColor,
                ears,
                hasGlasses,
                isFemale,
                true);

            yield return new WaitForSeconds(m_breedRate);
        }
    }

    public void Breed(NPC source, NPC target)
    {
        var amount = Random.Range(m_breedMin, m_breedMax);
        StartCoroutine(BreedRoutine(source, target, amount));
    }
}

