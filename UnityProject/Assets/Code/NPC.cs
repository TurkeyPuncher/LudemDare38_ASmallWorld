using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

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
    private Image m_eyelashesImage;

    [SerializeField]
    private Image m_glassesImage;

    [SerializeField]
    private Image m_browImage;

    [SerializeField]
    private Image m_hairImage;

    [SerializeField]
    private Image m_femaleHairImage;

    [SerializeField]
    private Image m_earImage;

    [SerializeField]
    private Image m_leftHandImage;

    [SerializeField]
    private Image m_rightHandImage;

    [Header("Movement")]
    [SerializeField]
    private Transform m_npcTransform;

    [SerializeField]
    private float m_walkSpeed = 0.1f;

    [SerializeField]
    MeshRenderer m_stateColorMeshRenderer = null;

    [SerializeField]
    bool m_showStateFeedback = true;

    [SerializeField]
    Animator m_animator = null;

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

    public bool IsFemale { get; private set; }
    public NPCFactory.FemaleHair FemaleHairType { get; private set; }
    public NPCFactory.Eyelashes EyelashesType { get; private set; }

    public List<string> Traits = new List<string>();
    public Transform NPCTransform { get { return m_npcTransform; } }

    private NPCFactory m_factory;
    private Animator m_aiStateMachine;
    private string[] m_startStateNames = { "Walk", "Idle", "ChangeDirection" };
    private bool m_inCollisionTrigger = false;

    void Start()
    {
        m_factory = NPCFactory.Instance;
        m_aiStateMachine = GetComponent<Animator>();
        // Pick a random state
        m_aiStateMachine.Play(m_startStateNames[UnityEngine.Random.Range(0, m_startStateNames.Length)]);

        m_stateColorMeshRenderer.enabled = m_showStateFeedback;
    }

    void OnTriggerEnter(Collider other)
    {
        Stop();
        if (m_inCollisionTrigger)
            return;

        m_inCollisionTrigger = true;
        // Check what we hit
        if (other.gameObject.CompareTag("NPC"))
        {
            MoveAway(transform.position - other.transform.position);
            m_aiStateMachine.SetTrigger("OnCollisionNPC");
        }
        else if (other.gameObject.CompareTag("Environment"))
        {
            MoveAway(transform.position - other.transform.position);
            m_aiStateMachine.SetTrigger("OnCollisionNPC");
        }
    }

    void OnTriggerExit(Collider other)
    {
        m_inCollisionTrigger = false;
    }

    public void SetTraitsAndLook(NPCFactory.Face face, Sprite faceSprite,
        NPCFactory.FaceColor faceColor, Color faceColorColor,
        NPCFactory.Mouth mouth, Sprite mouthSprite,
        NPCFactory.Nose nose, Sprite noseSprite,
        NPCFactory.Eyes eyes, Sprite eyesSprite,
        NPCFactory.Eyelashes eyelashes, Sprite eyeslashesSprite,
        NPCFactory.Glasses glasses, Sprite glassesSprite,
        NPCFactory.Brow brow, Sprite browSprite,
        NPCFactory.Hair hair, Sprite hairSprite,
        NPCFactory.FemaleHair femaleHair, Sprite femaleHairSprite,
        NPCFactory.HairColor hairColor, Color hairColorColor,
        NPCFactory.Ears ear, Sprite earSprite,
        bool hasGlasses,
        bool isFemale)
    {
        FaceType = face;
        Traits.Add(string.Format("{0}:{1}", face.GetType().Name, face));
        MouthType = mouth;
        Traits.Add(string.Format("{0}:{1}", mouth.GetType().Name, mouth));
        NoseType = nose;
        Traits.Add(string.Format("{0}:{1}", nose.GetType().Name, nose));
        EyesType = eyes;
        Traits.Add(string.Format("{0}:{1}", eyes.GetType().Name, eyes));
        BrowType = brow;
        Traits.Add(string.Format("{0}:{1}", brow.GetType().Name, brow));
        HairType = hair;
        Traits.Add(string.Format("{0}:{1}", hair.GetType().Name, hair));
        EarType = ear;
        Traits.Add(string.Format("{0}:{1}", ear.GetType().Name, ear));

        
        m_faceImage.sprite = faceSprite;
        m_faceImage.color = faceColorColor;
        m_mouthImage.sprite = mouthSprite;
        m_mouthImage.color = faceColorColor;
        m_noseImage.sprite = noseSprite;
        m_noseImage.color = faceColorColor;
        m_eyesImage.sprite = eyesSprite;
        m_eyesImage.color = faceColorColor;
        m_browImage.sprite = browSprite;
        m_browImage.color = faceColorColor;

        m_hairImage.sprite = hairSprite;
        m_hairImage.color = hairColorColor;

        m_earImage.sprite = earSprite;
        m_earImage.color = faceColorColor;

        m_leftHandImage.color = hairColorColor + Color.red + Color.black;
        m_rightHandImage.color = faceColorColor + Color.red;

        // Has glasses
        HasGlasses = hasGlasses;
        if (hasGlasses)
        {
            GlassesType = glasses;
            Traits.Add(string.Format("{0}_{1}", glasses.GetType().Name, glasses));

            m_glassesImage.sprite = glassesSprite;
            m_glassesImage.color = faceColorColor;
            m_glassesImage.enabled = true;
        }
        else
        {
            m_glassesImage.enabled = false;
        }

        // Is female
        IsFemale = isFemale;
        if (isFemale)
        {
            FemaleHairType = femaleHair;
            Traits.Add(string.Format("{0}:{1}", femaleHair.GetType().Name, femaleHair));
            EyelashesType = eyelashes;
            Traits.Add(string.Format("{0}:{1}", eyelashes.GetType().Name, eyelashes));
            
            m_femaleHairImage.enabled = true;
            m_femaleHairImage.sprite = femaleHairSprite;
            m_femaleHairImage.color = hairColorColor;
            m_eyelashesImage.enabled = true;
            m_eyelashesImage.sprite = eyeslashesSprite;
            m_eyelashesImage.color = faceColorColor;
        }
        else
        {
            m_femaleHairImage.enabled = false;
            m_eyelashesImage.enabled = false;
        }

        GameManager.Instance.AddPopulation();
    }

    public IEnumerator ChangeDirectionRoutine(float timeToChange)
    {
        // making sure we turn around completely
        var randomAngle = UnityEngine.Random.Range(-90f, 90f) + 180f;
        //randomAngle = randomAngle + ((randomAngle > 0) ? 180f : -180f);

        var deltaTime = 0.0;
        while (deltaTime < timeToChange)
        {
            deltaTime += Time.fixedDeltaTime;
            m_npcTransform.transform.RotateAround(Vector3.zero, m_npcTransform.transform.forward, randomAngle * Time.fixedDeltaTime / timeToChange);
            yield return new WaitForFixedUpdate();
        }

    }

    public void ChangeDirection(float timeInSeconds)
    {
        Stop();
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
        Stop();
        StartCoroutine(WalkCoroutine());
        m_animator.SetTrigger("Walk");
    }

    public void Idle()
    {
        m_animator.Play("Idle");
    }

    public IEnumerator MoveAwayCoroutine(Vector3 direction)
    {
        m_npcTransform.transform.LookAt(transform, direction);
        while (true)
        {
            m_npcTransform.transform.RotateAround(Vector3.zero, m_npcTransform.transform.right, -m_walkSpeed * 3f);
            yield return new WaitForEndOfFrame();
        }
    }

    public void MoveAway(Vector3 direction)
    {
        Stop();
        StartCoroutine(MoveAwayCoroutine(direction));
    }

    public void Attack()
    {
        m_animator.SetTrigger("Attack");
    }

    public void Stop()
    {
        m_animator.SetTrigger("Stop");
        StopAllCoroutines();
    }

    public void SetStateColor(Color color)
    {
        m_stateColorMeshRenderer.material.color = color;
    }

    public void Dead()
    {
        GameManager.Instance.RemovePopulation();
    }
}
