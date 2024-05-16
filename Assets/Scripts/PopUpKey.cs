using UnityEngine;
using UnityEngine.UI;

public class PopUpKey : MonoBehaviour
{
    private Button m_button;
    [TextArea]
    [SerializeField] string m_text;
    private void Start()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(SendToPopUp);
    }
    private void SendToPopUp()
    {
        FindObjectOfType<PopupLock>().DoPopUp(m_text);
    }
}