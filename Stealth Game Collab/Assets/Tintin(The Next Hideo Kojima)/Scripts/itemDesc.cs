using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDesc : MonoBehaviour
{
    public PickupSystem pickupsystem;
    public GameObject sodacantext;
    public GameObject bodyspraytext;
    public GameObject MustacheComb;
    public GameObject itemdescWindow;
    public bool hasAlreadyPickedupSoda = false;
    public bool hasAlreadyPickedupBodySpray = false;
    public bool hasAlreadyPickedupComb = false;
    // Start is called before the first frame update
    void Start()
    {
        sodacantext.SetActive(false);
        bodyspraytext.SetActive(false);
        MustacheComb.SetActive(false);
        itemdescWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
