using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour
{
    private NPC m_npc;

    [SerializeField]
    NPCFactory.Face face;
    [SerializeField]
    NPCFactory.FaceColor faceColor;
    [SerializeField]
    NPCFactory.Mouth mouth;
    [SerializeField]
    NPCFactory.Nose nose;
    [SerializeField]
    NPCFactory.Eyes eyes;
    [SerializeField]
    NPCFactory.Eyebrows eyebrows;
    [SerializeField]
    NPCFactory.Hair hair;
    [SerializeField]
    NPCFactory.HairColor hairColor;
    [SerializeField]
    NPCFactory.Ears ears;
    [SerializeField]
    bool hasGlasses = false;
    [SerializeField]
    bool isFemale = false;

    // Use this for initialization
    void Start ()
    {
        m_npc = GetComponent<NPC>();
        m_npc.SetTraitsAndLook(
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
            false);
        m_npc.m_animator.Play("Walk");
    }
}
