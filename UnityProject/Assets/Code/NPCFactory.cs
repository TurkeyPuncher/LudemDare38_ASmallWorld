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
    public Sprite[] m_femaleEyelashesSprites = null;

    [SerializeField]
    public Sprite[] m_glassesSprites = null;

    [SerializeField]
    public Sprite[] m_browSprites = null;

    [SerializeField]
    public Sprite[] m_hairSprites = null;

    [SerializeField]
    public Sprite[] m_femaleHairSprites = null;

    [SerializeField]
    public Color[] m_hairColor = null;

    [SerializeField]
    public Sprite[] m_earSprites = null;

    public enum Face : byte
    {
        Potato,
        Cabbage,
        Turnip,
        Rock,
    }

    public enum FaceColor : byte
    {
        Yellow,
        Orange,
        Brown,
        Blue,
        Purple,
        Pink,
        Green,
    }

    public enum Mouth : byte
    {
        Happy,
        Neutral,
        Sad,
        Angry,
    }

    public enum Nose : byte
    {
        Small,
        Pointy,
        Snout,
    }

    public enum Eyes : byte
    {
        Snoopy,
        Asian,
        Disney,
        Droopy,
    }

    public enum Eyelashes : byte
    {
        Normal,
    }

    public enum Glasses : byte
    {
        Regular,
    }

    public enum Eyebrows : byte
    {
        Regular,
        Bushy,
        Uni,
        Thin,
    }

    public enum Hair : byte
    {
        Pineapple,
        Fabio,
        FlatTop,
        Curly,
    }

    public enum FemaleHair : byte
    {
        Buns,
        Long,
    }

    public enum HairColor : byte
    {
        Black,
        Brown,
        Yellow,
        White,
        Red,
        Green,
        Blue,
    }

    public enum Ears : byte
    {
        Regular,
        Elf,
        Dumbo,
    }

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

    public NPC CreateNPC(Vector3 position,
        NPCFactory.Face face, 
        NPCFactory.FaceColor faceColor, 
        NPCFactory.Mouth mouth, 
        NPCFactory.Nose nose, 
        NPCFactory.Eyes eyes, 
        NPCFactory.Eyelashes eyelashes, 
        NPCFactory.Glasses glasses, 
        NPCFactory.Eyebrows eyebrows,
        NPCFactory.Hair hair, 
        NPCFactory.FemaleHair femaleHair, 
        NPCFactory.HairColor hairColor, 
        NPCFactory.Ears ears, 
        bool hasGlasses,
        bool isFemale,
        bool isBaby)
    {
        // Instantiate
        var go = Instantiate(m_npcPrefab, transform, false) as GameObject;
        NPC npc = go.GetComponentInChildren<NPC>();

        npc.SetTraitsAndLook(
            face, NPCFactory.Instance.m_faceSprites[(int)face],
            faceColor, NPCFactory.Instance.m_faceColor[(int)faceColor],
            mouth, NPCFactory.Instance.m_mouthSprites[(int)mouth],
            nose, NPCFactory.Instance.m_noseSprites[(int)nose],
            eyes, NPCFactory.Instance.m_eyesSprites[(int)eyes],
            0, NPCFactory.Instance.m_femaleEyelashesSprites[0],
            0, NPCFactory.Instance.m_glassesSprites[0],
            eyebrows, NPCFactory.Instance.m_browSprites[(int)eyebrows],
            hair, NPCFactory.Instance.m_hairSprites[(int)hair],
            0, NPCFactory.Instance.m_femaleHairSprites[0],
            hairColor, NPCFactory.Instance.m_hairColor[(int)hairColor],
            ears, NPCFactory.Instance.m_earSprites[(int)ears],
            hasGlasses,
            isFemale,
            isBaby);

        // Convert position
        npc.NPCTransform.LookAt(position);
        return npc;
    }

    public NPC CreateNPC(Vector3 position)
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
        int brow = Random.Range(0, m_browSprites.Length);
        int hair = Random.Range(0, m_hairSprites.Length);
        int hairColor = Random.Range(0, m_hairColor.Length);
        int ear = Random.Range(0, m_earSprites.Length);

        bool hasGlasses = (Random.Range(0, 100) > 10) ? false : true;
        int glasses = Random.Range(0, m_glassesSprites.Length);

        bool isFemale = (Random.Range(0, 100) > 50) ? false : true;
        int femaleHair = Random.Range(0, m_femaleHairSprites.Length);
        int eyelashes = Random.Range(0, m_femaleEyelashesSprites.Length);

        npc.SetTraitsAndLook((Face)face, m_faceSprites[face],
            (FaceColor)faceColor, m_faceColor[faceColor],
            (Mouth)mouth, m_mouthSprites[mouth],
            (Nose)nose, m_noseSprites[nose],
            (Eyes)eyes, m_eyesSprites[eyes],
            (Eyelashes)eyelashes, m_femaleEyelashesSprites[eyelashes],
            (Glasses)glasses, m_glassesSprites[glasses],
            (Eyebrows)brow, m_browSprites[brow],
            (Hair)hair, m_hairSprites[hair],
            (FemaleHair)femaleHair, m_femaleHairSprites[femaleHair],
            (HairColor)hairColor, m_hairColor[hairColor],
            (Ears)ear, m_earSprites[ear],
            hasGlasses,
            isFemale,
            true);

        // Convert position
        npc.NPCTransform.LookAt(position);
        return npc;
    }
}
