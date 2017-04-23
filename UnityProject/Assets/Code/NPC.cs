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

    public NPCFactory.Face FaceType { get; private set; }
    public NPCFactory.FaceColor FaceColorType { get; private set; }
    public NPCFactory.Mouth MouthType { get; private set; }
    public NPCFactory.Nose NoseType { get; private set; }
    public NPCFactory.Eyes EyesType { get; private set; }
    public NPCFactory.Glasses GlassesType { get; private set; }
    public NPCFactory.Brow BrowType { get; private set; }
    public NPCFactory.Hair HairType { get; private set; }
    public NPCFactory.HairColor HairColorType { get; private set; }
    public NPCFactory.Ears EarType { get; private set; }
    public bool HasGlasses { get; private set; }

    public Transform NPCTransform { get { return m_npcTransform; } }

    private NPCFactory m_factory;
    
    void Start()
    {
        m_factory = NPCFactory.Instance;
    }

    public void SetTraitsAndLook(NPCFactory.Face face, Sprite faceSprite,
        NPCFactory.FaceColor faceColor, Color faceColorColor,
        NPCFactory.Mouth mouth, Sprite mouthSprite,
        NPCFactory.Nose nose, Sprite noseSprite,
        NPCFactory.Eyes eyes, Sprite eyesSprite,
        NPCFactory.Glasses glasses, Sprite glassesSprite,
        NPCFactory.Brow brow, Sprite browSprite,
        NPCFactory.Hair hair, Sprite hairSprite,
        NPCFactory.HairColor hairColor, Color hairColorColor,
        NPCFactory.Ears ear, Sprite earSprite,
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
