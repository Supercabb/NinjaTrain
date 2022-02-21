using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHandR : MonoBehaviour
{
    public GameObject toDisable;
    public GameObject toDisableKatanaTouch;
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
            toDisableKatanaTouch.SetActive(true);
        }
        else if (OVRInput.GetActiveController() == OVRInput.Controller.Hands)
        {
            toDisable.SetActive(true);
            toDisableKatanaTouch.SetActive(false);
        }
    }
}
