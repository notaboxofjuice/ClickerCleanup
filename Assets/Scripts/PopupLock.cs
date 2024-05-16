using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PopupLock : MonoBehaviour
{
    public GameObject PopUpPanel;
    public TextMeshProUGUI PopUpText;
    [SerializeField] Button m_closeButton;
    private void Start()
    {
        m_closeButton.onClick.AddListener(CleanUp);
    }
    private void CleanUp()
    {
        PopUpText.text = "";
        PopUpPanel.SetActive(false);
    }
    public void DoPopUp(string text)
    {
        PopUpText.text = text;
        PopUpPanel.SetActive(true);
    }
}