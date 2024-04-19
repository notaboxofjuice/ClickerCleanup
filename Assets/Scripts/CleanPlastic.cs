using UnityEngine;
public class CleanPlastic : MonoBehaviour
{
    public void DoCleaning(float amount)
    {
        GameManager.Instance.Clean(amount);
    }
}