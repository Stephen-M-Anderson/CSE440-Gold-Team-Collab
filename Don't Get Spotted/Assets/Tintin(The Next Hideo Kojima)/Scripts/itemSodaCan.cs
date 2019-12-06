using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSodaCan : MonoBehaviour
{
    public GameObject sodaCan;
    public bool yesitsaSodacan;
    public void sodaCans()
    {
        if (yesitsaSodacan)
        {
            DontDestroyOnLoad(sodaCan);
        }
    }
}
