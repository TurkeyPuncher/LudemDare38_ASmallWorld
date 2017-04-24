using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    [Header("Avatar Image")]
    [SerializeField]
    private RectTransform m_canvasTransform;

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
    private float m_growthTime = 5f;

    [SerializeField]
    MeshRenderer m_stateColorMeshRenderer = null;

    [SerializeField]
    bool m_showStateFeedback = true;

    [SerializeField]
    public Animator m_animator = null;

    [SerializeField]
    Transform m_loveGroup = null;

    [SerializeField]
    GameObject m_lovePrefab = null;

    [SerializeField]
    List<BehaviourTrait.Trigger> m_loveTraits = new List<BehaviourTrait.Trigger>();

    [SerializeField]
    List<BehaviourTrait.Trigger> m_hateTraits = new List<BehaviourTrait.Trigger>();
    
    [SerializeField]
    public List<BehaviourTrait.Trigger> m_traits = new List<BehaviourTrait.Trigger>();
    
    [SerializeField]
    private AnimCurveScale m_babyScaler;

    [SerializeField]
    private List<string> m_debugTraits = new List<string>();

    public List<BehaviourTrait.Trigger> Traits { get { return m_traits; } }
    
    public NPCFactory.Face FaceType { get; private set; }
    public NPCFactory.FaceColor FaceColorType { get; private set; }
    public NPCFactory.Mouth MouthType { get; private set; }
    public NPCFactory.Nose NoseType { get; private set; }
    public NPCFactory.Eyes EyesType { get; private set; }
    public NPCFactory.Glasses GlassesType { get; private set; }
    public NPCFactory.Eyebrows BrowType { get; private set; }
    public NPCFactory.Hair HairType { get; private set; }
    public NPCFactory.HairColor HairColorType { get; private set; }
    public NPCFactory.Ears EarType { get; private set; }
    public bool HasGlasses { get; private set; }

    public bool IsFemale { get; private set; }
    public NPCFactory.FemaleHair FemaleHairType { get; private set; }
    public NPCFactory.Eyelashes EyelashesType { get; private set; }

    public Transform NPCTransform { get { return m_npcTransform; } }

    public Collider CollidedWith { get; private set; }
    
    private Animator m_aiStateMachine;
    private bool m_inCollisionTrigger = false;

    private Camera m_mainCamera;
    private Vector3 m_lastScreenPosition;

    private CapsuleCollider m_collider;

    void Start()
    {
        m_aiStateMachine = GetComponent<Animator>();

        m_stateColorMeshRenderer.enabled = m_showStateFeedback;
        m_mainCamera = GameManager.Instance.MainCamera;
        m_lastScreenPosition = m_mainCamera.WorldToScreenPoint(transform.position);
        m_collider = GetComponent<CapsuleCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        CollidedWith = other;
        if (m_inCollisionTrigger)
            return;
        Stop();

        m_inCollisionTrigger = true;
        // Check what we hit
        if (other.gameObject.CompareTag("NPC"))
        {
            HandleOtherNPC(other.GetComponent<NPC>());
        }
        else if (other.gameObject.CompareTag("Environment"))
        {
            MoveAway(transform.position - other.transform.position);
            m_aiStateMachine.SetTrigger("AfterEvent");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        m_inCollisionTrigger = false;
    }

    public void AddTrait(Enum trait)
    {
        var traitString = string.Format("{1}_{0}", trait.GetType().Name, trait);
        m_debugTraits.Add(traitString);
        if (Enum.IsDefined(typeof(BehaviourTrait.Trigger), traitString))
        {
            var trigger = (BehaviourTrait.Trigger)Enum.Parse(typeof(BehaviourTrait.Trigger), traitString);
            m_traits.Add(trigger);
        }
    }

    public void SetTraitsAndLook(NPCFactory.Face face, Sprite faceSprite,
        NPCFactory.FaceColor faceColor, Color faceColorColor,
        NPCFactory.Mouth mouth, Sprite mouthSprite,
        NPCFactory.Nose nose, Sprite noseSprite,
        NPCFactory.Eyes eyes, Sprite eyesSprite,
        NPCFactory.Eyelashes eyelashes, Sprite eyeslashesSprite,
        NPCFactory.Glasses glasses, Sprite glassesSprite,
        NPCFactory.Eyebrows brow, Sprite browSprite,
        NPCFactory.Hair hair, Sprite hairSprite,
        NPCFactory.FemaleHair femaleHair, Sprite femaleHairSprite,
        NPCFactory.HairColor hairColor, Color hairColorColor,
        NPCFactory.Ears ear, Sprite earSprite,
        bool hasGlasses,
        bool isFemale,
        bool isBaby)
    {
        FaceType = face;
        MouthType = mouth;
        NoseType = nose;
        EyesType = eyes;
        BrowType = brow;
        HairType = hair;
        EarType = ear;

        AddTrait(face);
        AddTrait(faceColor);
        // AddTrait(mouth); Not in use
        AddTrait(nose);
        //AddTrait(eyes); Not in use
        //AddTrait(eyelashes); Added later
        //AddTrait(glasses); Added later
        AddTrait(brow);
        AddTrait(hair);
        //AddTrait(femaleHair); Added later
        AddTrait(hairColor);
        AddTrait(ear);

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
            AddTrait(glasses);

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
            AddTrait(femaleHair);
            EyelashesType = eyelashes;
            AddTrait(eyelashes);
            
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

        GameManager.Instance.AddPopulation(this);

        if(isBaby)
        {
            DisableColliderForTime(m_growthTime);
            if (m_babyScaler != null)
            {
                m_babyScaler.m_duration = m_growthTime;
                m_babyScaler.enabled = true;
            }
        }
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
            SetImageDirection();
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
            SetImageDirection();
            yield return new WaitForEndOfFrame();
        }
    }

    public void MoveAway(Vector3 direction)
    {
        Stop();
        StartCoroutine(MoveAwayCoroutine(direction));
    }

    public void Attack(float timeInSeconds)
    {
        Stop();
        DisableColliderForTime(timeInSeconds);
        m_animator.SetTrigger("Attack");
    }

    IEnumerator MakeLoveRoutine(float timeInSeconds)
    {
        float deltaTime = 0f;
        while (deltaTime < timeInSeconds)
        {
            deltaTime += Time.deltaTime;
            Instantiate(m_lovePrefab, m_loveGroup, false);
            yield return new WaitForSeconds(1f);
        }
    }

    public void MakeLove(float timeInSeconds)
    {
        Stop();
        DisableColliderForTime(timeInSeconds);
        m_animator.SetTrigger("Love");
        StartCoroutine(MakeLoveRoutine(timeInSeconds));
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

    public void SetImageDirection()
    {
        var direction = m_mainCamera.WorldToScreenPoint(transform.position) - m_lastScreenPosition;
        m_canvasTransform.localScale = (direction.x > 0) ? new Vector3(-1f, 1f, 1f) : Vector3.one;
        m_lastScreenPosition = m_mainCamera.WorldToScreenPoint(transform.position);
    }

    public void SetMood()
    {
        if (m_loveTraits.Count > m_hateTraits.Count)
        {
            m_mouthImage.sprite = NPCFactory.Instance.m_mouthSprites[(int)NPCFactory.Mouth.Happy];
        }
        else if (m_loveTraits.Count + 2 < m_hateTraits.Count)
        {
            m_mouthImage.sprite = NPCFactory.Instance.m_mouthSprites[(int)NPCFactory.Mouth.Angry];
        }
        else if (m_loveTraits.Count < m_hateTraits.Count)
        {
            m_mouthImage.sprite = NPCFactory.Instance.m_mouthSprites[(int)NPCFactory.Mouth.Sad];
        }
        else if (m_loveTraits.Count == m_hateTraits.Count)
        {
            m_mouthImage.sprite = NPCFactory.Instance.m_mouthSprites[(int)NPCFactory.Mouth.Neutral];
        }
    }

    public void AddLove(BehaviourTrait love)
    {
        if (m_traits.Contains(love.Source))
        {
            if(!m_loveTraits.Contains(love.Target))
            { 
                m_loveTraits.Add(love.Target);
                SetMood();
            }
        }
    }
    public void AddHate(BehaviourTrait hate)
    {
        if (m_traits.Contains(hate.Source))
        {
            if (!m_hateTraits.Contains(hate.Target))
            {
                m_hateTraits.Add(hate.Target);
                SetMood();
            }
        }
    }

    public bool HasLoveFor(NPC other)
    {
        // Love only works on opposite gender
        if (IsFemale == other.IsFemale)
            return false;

        foreach (var traits in other.Traits)
            if (m_loveTraits.Contains(traits))
                return true;
        return false;
    }
    
    public bool HasHateFor(NPC other)
    {
        foreach (var traits in other.Traits)
            if (m_hateTraits.Contains(traits))
                return true;
        return false;
    }

    // Called on trigger if collider is an NPC
    public void HandleOtherNPC(NPC otherNPC)
    {
        if (HasLoveFor(otherNPC))
        {
            // Make both NPCs go to love state
            otherNPC.ForceLove();
            m_aiStateMachine.SetTrigger("Love");

            // Tell Game Manager to spawn a new NPC
            GameManager.Instance.Breed(this, otherNPC);
        }
        else if (HasHateFor(otherNPC))
        {
            otherNPC.ForceAttack();
            m_aiStateMachine.SetTrigger("Attack");

            // Tell Game Manager to kill both NPCs
            //GameManager.Instance.Explode(this);
        }
        else
        {
            MoveAway(transform.position - otherNPC.transform.position);
            m_aiStateMachine.SetTrigger("AfterEvent");
        }
    }

    // called by other npcs
    public void ForceLove()
    {
        m_aiStateMachine.SetTrigger("Love");
    }

    // called by other npcs
    public void ForceAttack()
    {
        m_aiStateMachine.SetTrigger("Attack");
    }

    public void DisableColliderForTime(float timeInSeconds)
    {
        m_collider = GetComponent<CapsuleCollider>();
        m_collider.enabled = false;
        Invoke("EnableCollider", timeInSeconds);
    }

    public void EnableCollider()
    {
        m_collider.enabled = true;
    }
}
