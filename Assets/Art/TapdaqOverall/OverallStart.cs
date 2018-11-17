using UnityEngine;
using System.Collections;

public class OverallStart : MonoBehaviour
{
    public void OnEnable()
    {
        if (!TapdaqManager.Instance.IsCPLoaded(AdTypes.CPOverAll, AdSizes.Small))
        {
            Invoke("ShowOverallOnMainMenu", 5);
        }
        else if (GameObject.FindObjectOfType<TapdaqUIManager>())
            GameObject.FindObjectOfType<TapdaqUIManager>().ShowOverAll();
    }

    public void ShowOverallOnMainMenu()
    {
        if (GameObject.FindObjectOfType<TapdaqUIManager>())
            GameObject.FindObjectOfType<TapdaqUIManager>().ShowOverAll();
    }
}
