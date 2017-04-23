using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [Header("Avatar Image")]
    [SerializeField]
    private Image m_faceImage;

    [SerializeField]
    private Image m_mouthImage;

    [SerializeField]
    private Image m_noseImage;

    [SerializeField]
    private Image m_eyesImage;

    [SerializeField]
    private Image m_glassesImage;

    [SerializeField]
    private Image m_browImage;

    [SerializeField]
    private Image m_hairImage;

    [SerializeField]
    private Image m_earImage;

    [Header("Movement")]
    [SerializeField]
    private Transform m_npcTransform;

    public Face FaceType { get; private set; }
    public FaceColor FaceColorType { get; private set; }
    public Mouth MouthType { get; private set; }
    public Nose NoseType { get; private set; }
    public Eyes EyesType { get; private set; }
    public Glasses GlassesType { get; private set; }
    public Brow BrowType { get; private set; }
    public Hair HairType { get; private set; }
    public HairColor HairColorType { get; private set; }
    public Ears EarType { get; private set; }
    public bool HasGlasses { get; private set; }

    public Transform NPCTransform { get { return m_npcTransform; } }

    private NPCFactory m_factory;

    public enum Face : byte
    {
        Regular,
        Fat,
    }

    public enum FaceColor : byte
    {
        Pink,
        Yellow,
        Brown,
        Blue,
        Purple,
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
        Normal,
        Big,
        Snout,
    }

    public enum Eyes : byte
    {
        Normal,
        Asian,
        Disney,
        Droopy,
    }

    public enum Glasses : byte
    {
        Normal,
    }

    public enum Brow : byte
    {
        Regular,
        Fuzzy,
        Unibrow,
        Thin,
    }

    public enum Hair : byte
    {
        Regular,
        Fabio,
        FlatTop,
        Curly,
    }

    public enum HairColor : byte
    {
        Black,
        Brown,
        Yellow,
        White,
        Red,
    }

    public enum Ears : byte
    {
        Regular,
        Fabio,
        FlatTop,
        Curly,
    }

    void Awake()
    {
        m_factory = NPCFactory.Instance;
    }

    public void SetTraitsAndLook(Face face, Sprite faceSprite,
        FaceColor faceColor, Color faceColorColor,
        Mouth mouth, Sprite mouthSprite,
        Nose nose, Sprite noseSprite,
        Eyes eyes, Sprite eyesSprite,
        Glasses glasses, Sprite glassesSprite,
        Brow brow, Sprite browSprite,
        Hair hair, Sprite hairSprite,
        HairColor hairColor, Color hairColorColor,
        Ears ear, Sprite earSprite,
        bool hasGlasses)
    {
        FaceType = face;
        MouthType = mouth;
        NoseType = nose;
        EyesType = eyes;
        GlassesType = glasses;
        BrowType = brow;
        HairType = hair;
        EarType = ear;
        HasGlasses = hasGlasses;

        m_faceImage.sprite = faceSprite;
        m_faceImage.color = faceColorColor;
        m_mouthImage.sprite = mouthSprite;
        m_mouthImage.color = faceColorColor;
        m_noseImage.sprite = noseSprite;
        m_noseImage.color = faceColorColor;
        m_eyesImage.sprite = eyesSprite;
        m_eyesImage.color = faceColorColor;
        m_glassesImage.sprite = glassesSprite;
        m_glassesImage.color = faceColorColor;
        m_browImage.sprite = browSprite;
        m_browImage.color = faceColorColor;

        m_hairImage.sprite = hairSprite;
        m_hairImage.color = hairColorColor;

        m_earImage.sprite = earSprite;
        m_earImage.color = faceColorColor;

        m_glassesImage.enabled = (hasGlasses) ? true : false;
    }
}
