using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertHUD : MonoBehaviour
{
    // Start is called before the first frame update
    //public FlashlightDetection flashlightAlert;
    public GetCaughtDetection getcaughtscript;
    public Image alert;
    int hasAlerted = 0;
    float waitTimeNumber = 5;

    private void Start()
    {
        StartCoroutine(waitandStuff());
    }
    // Update is called once per frame
    void Update()
    {
        if (getcaughtscript.Sighted)                                     // If an enemy spotlight collides with you
                                                                                    //GET SPOTTED SON and the alert should change
        {
            alert.color = UnityEngine.Color.red;
            Debug.Log("IN VIEW IN VIEW");
            hasAlerted++;
        }

        else if (hasAlerted == 1)
        {
            waitandStuff();
        }
        else if (hasAlerted > 1)
        {

        }



    }

    void calmDownPeriod()
    {
        alert.color = UnityEngine.Color.yellow;
    }

    private IEnumerator waitandStuff()
    {
        Debug.Log("BEGINNING THE WAIT");
        yield return new WaitForSeconds(waitTimeNumber);

        Debug.Log("END OF WAIT");
        calmDownPeriod();
        hasAlerted = 0;
    }
}