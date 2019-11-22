using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertHUD : MonoBehaviour
{
    // Start is called before the first frame update
    public FlashlightDetection flashlightAlert;
    public Image alert;
    // Update is called once per frame
    void Update()
    {
        if(flashlightAlert.enemyScript.inView)
        {
            alert.color = UnityEngine.Color.red;
            Debug.Log("IN VIEW IN VIEW");
        }
    }
}
