using UnityEngine;
using System.Collections;

public class Quality : MonoBehaviour
{
    public string[] names;

    void Awake()
    {
        names = QualitySettings.names;
        if (SystemInfo.systemMemorySize < 1500)
        {
            int i = 0;
            while (i < names.Length)
            {
                if (names[i].Equals("Fastest"))
                    QualitySettings.SetQualityLevel(i, true);

                i++;
            } 
        }
    }
}
