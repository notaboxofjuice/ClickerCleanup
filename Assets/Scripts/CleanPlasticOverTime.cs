using System.Collections;
using UnityEngine;
public class CleanPlasticOverTime : MonoBehaviour
{
    private IEnumerable CleanPlasticRoutine(float amount, float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.Clean(amount);
    }
    public void CleanOverTime(float amount, float time)
    {
        StartCoroutine(nameof(CleanPlasticRoutine), (amount, time));
    }
}