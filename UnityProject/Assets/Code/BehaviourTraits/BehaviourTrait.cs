using UnityEngine;

using System;

[Serializable]
public class BehaviourTrait
{
    [SerializeField]
    protected string m_traitMessage = "";

    [SerializeField]
    protected Trigger m_sourceTrigger;

    [SerializeField]
    protected Trigger m_targetTrigger;
    
    public enum Trigger : int
    {
        Blue_FaceColor,
        Purple_FaceColor,
        Green_FaceColor,
        Orange_FaceColor,
        Small_Nose, //working
        Pointy_Nose, //working
        Snout_Nose, //working
        Bushy_Eyebrows,
        Uni_Eyebrows,
        Thin_Eyebrows,
        Pineapple_Hair, //working
        Fabio_Hair, //working
        FlatTop_Hair, //working
        Curly_Hair, //working
        Red_HairColor,
        Green_HairColor,
        Blue_HairColor,
        Elf_Ears, //working
        Dumbo_Ears, //working
        Regular_Glasses, //working
        _Count,
    }

    public Trigger Source { get { return m_sourceTrigger; } }
    public Trigger Target { get { return m_targetTrigger; } }

    public Trigger RandomTrigger()
    {
        int count = (int)Trigger._Count;
        return (Trigger)UnityEngine.Random.Range(0, count);
    }

    // Generates the trait description
    virtual public void GenerateMessage()
    {
    }
}