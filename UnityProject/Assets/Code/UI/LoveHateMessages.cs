using UnityEngine;
using UnityEngine.UI;

public class LoveHateMessages : MonoBehaviour
{
    [SerializeField]
    Transform m_loveGroup = null;

    [SerializeField]
    Transform m_hateGroup = null;

    [SerializeField]
    Text m_loveMessagePrefab = null;

    [SerializeField]
    Text m_hateMessagePrefab = null;

    public void AddLove(string message)
    {
        var text = Instantiate(m_loveMessagePrefab, m_loveGroup, false) as Text;
        text.text = message;
    }

    public void AddHate(string message)
    {
        var text = Instantiate(m_hateMessagePrefab, m_hateGroup, false) as Text;
        text.text = message;
    }
}
