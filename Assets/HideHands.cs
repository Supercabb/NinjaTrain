using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHands : MonoBehaviour
{

    public GameObject toDisable;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        //Debug.LogError("EIEIEI");     
        if (OVRInput.GetActiveController() == OVRInput.Controller.Touch)
        {
            toDisable.SetActive(false);
        }
        else if(OVRInput.GetActiveController() == OVRInput.Controller.Hands)
        {
            toDisable.SetActive(true);
        }
    }
}
