using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject m_npcPrefab = null;

    [SerializeField]
    public Sprite[] m_faceSprites = null;

    [SerializeField]
    public Color[] m_faceColor = null;

    [SerializeField]
    public Sprite[] m_mouthSprites = null;

    [SerializeField]
    public Sprite[] m_noseSprites = null;

    [SerializeField]
    public Sprite[] m_eyesSprites = null;

    [SerializeField]
    public Sprite[] m_glassesSprites = null;

    [SerializeField]
    public Sprite[] m_browSprites = null;

    [SerializeField]
    public Sprite[] m_hairSprites = null;

    [SerializeField]
    public Color[] m_hairColor = null;

    [SerializeField]
    public Sprite[] m_earSprites = null;

    private static NPCFactory m_instance;
    public static NPCFactory Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject newObj = new GameObject("NPCFactory");
                m_instance = newObj.AddComponent<NPCFactory>();
            }
            return m_instance;
        }
    }

    void Awake()
    {
        // Instance
        if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
    }

    public GameObject CreateNPC(Vector3 position)
    {
        // Instantiate
        var go = Instantiate(m_npcPrefab, transform, false) as GameObject;
        NPC npc = go.GetComponentInChildren<NPC>();

        // Randomize Look
        int face = Random.Range(0, m_faceSprites.Length);
        int faceColor = Random.Range(0, m_faceColor.Length);
        int mouth = Random.Range(0, m_mouthSprites.Length);
        int nose = Random.Range(0, m_noseSprites.Length);
        int eyes = Random.Range(0, m_eyesSprites.Length);
        int glasses = Random.Range(0, m_glassesSprites.Length);
        bool hasGlasses = (Random.Range(0, 100) > 10) ? false : true;
        int brow = Random.Range(0, m_browSprites.Length);
        int hair = Random.Range(0, m_hairSprites.Length);
        int hairColor = Random.Range(0, m_hairColor.Length);
        int ear = Random.Range(0, m_earSprites.Length);

        npc.SetTraitsAndLook((NPC.Face)face, m_faceSprites[face],
            (NPC.FaceColor)faceColor, m_faceColor[faceColor],
            (NPC.Mouth)mouth, m_mouthSprites[mouth],
            (NPC.Nose)nose, m_noseSprites[nose],
            (NPC.Eyes)eyes, m_eyesSprites[eyes],
            (NPC.Glasses)glasses, m_glassesSprites[glasses],
            (NPC.Brow)brow, m_browSprites[brow],
            (NPC.Hair)hair, m_hairSprites[hair],
            (NPC.HairColor)hairColor, m_hairColor[hairColor],
            (NPC.Ears)ear, m_earSprites[ear],
            hasGlasses
            );

        // Convert position
        npc.NPCTransform.LookAt(position);
        return null;
    }
}
