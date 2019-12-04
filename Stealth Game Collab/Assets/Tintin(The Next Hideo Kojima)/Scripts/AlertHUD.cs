using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertHUD : MonoBehaviour
{
    public Color redColor, yellowColor;
    Color currentColor;
    MeshRenderer coloerer;
    // Start is called before the first frame update
    public GetCaughtDetection getcaughtbool;
    public Image alert;
    int hasAlerted = 0;
    float waitTimeNumber = 5.0f;
    bool doingthings = false; // This bool will STOP the fucking courtine function from doings things

    private void Start()
    {
        
        //wStartCoroutine(waitandStuff(doingthings));
    }
    // Update is called once per frame
    void Update()
    {
        if (getcaughtbool.Sighted)  //IF SIGHTED by enemy
        {
            alert.color = UnityEngine.Color.red;   //MAKE THE HUD SCARY RED
            Debug.Log("IN VIEW IN VIEW");
            hasAlerted = 1;
            /*
            if(hasAlerted == 1)
            {
                FindObjectOfType<AudioManager>().Play("AlertMusic");
            }
            hasAlerted++;
            */
            //StartCoroutine(waitandStuff());
        }
        
        else if (!getcaughtbool.Sighted && hasAlerted == 1)
        {
            //StartCoroutine(waitandStuff(doingthings));
            //doingthings = true;
            //Debug.Log("ITS TIME TO CHANGE COLOR");
            
            alert.color = Color.Lerp(alert.color, Color.yellow, 0.01f);
            
        }
        


    }
    /*
    private IEnumerator waitandStuff()
    { 
            Debug.Log("BEGINNING THE WAIT which equals" + waitTimeNumber);
            yield return new WaitForSeconds(waitTimeNumber);
            Debug.Log("Done waiting");
            doingthings = false;
            //calmDownPeriod()
    }
    
    */
}
