using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    [SerializeField]
    private float m_walkSpeed = 0.1f;


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
    private Animator m_aiStateMachine;

    void Start()
    {
        m_factory = NPCFactory.Instance;
        m_aiStateMachine = GetComponent<Animator>();
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
    
    public IEnumerator ChangeDirectionRoutine(float timeToChange)
    {
        var randomAngle = Random.Range(-180f, 180f);

        var deltaTime = 0.0;
        while (deltaTime < timeToChange)
        {
            deltaTime += Time.deltaTime;
            m_npcTransform.transform.RotateAround(Vector3.zero, m_npcTransform.transform.forward, randomAngle * Time.deltaTime / timeToChange);
            yield return new WaitForEndOfFrame();
        }

    }

    public void ChangeDirection(float timeInSeconds)
    {
        StartCoroutine(ChangeDirectionRoutine(timeInSeconds));
    }   
    
    public IEnumerator WalkCoroutine()
    {
        while (true)
        {
            m_npcTransform.transform.RotateAround(Vector3.zero, m_npcTransform.transform.right, m_walkSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    public void Walk()
    {
        StopAllCoroutines();
        StartCoroutine(WalkCoroutine());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    public void OnCollisionEnter(Collision collision)
    {
        // Check if 
        if(collision.gameObject.CompareTag("NPC"))
            m_aiStateMachine.SetTrigger("OnCollisionNPC");
        else if(collision.gameObject.CompareTag("Floor")||
            collision.gameObject.CompareTag("Environment"))
            m_aiStateMachine.SetTrigger("OnCollisionNPC");
    }
}
