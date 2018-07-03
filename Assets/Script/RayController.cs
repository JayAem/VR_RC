using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour {

    public GameObject ray;
    string condition;
    RaycastHit hit;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    //variables for controller buttons
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    //public bool gripButtonPressed = false;


    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    //public bool triggerButtonPressed = false;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        ray = GameObject.FindWithTag("ray");
        ray.GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        string cond = onCall();
        if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit))
        {
            ShowLaser();
            if (hit.transform.tag == "Interactive")
            {
                if (cond == "true")
                {
                    Debug.Log(" Attach True ");
                    Quaternion rotation = hit.transform.localRotation;
                    Vector3 scale = hit.transform.localScale;
                    hit.transform.SetParent(trackedObj.transform.parent, true);
                }

                if (cond == "false")
                {
                    Debug.Log("Attach False");
                    hit.transform.parent = null;
                }
            }
        }
    }

    //key press method
    public string onCall()
    {
        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);

        if (gripButtonDown)
        {
            condition = "true";
        }
        if (gripButtonUp)
        {
            condition = "false";
        }
        return condition;
    }

    
    //show laser method
    private void ShowLaser()
    {
        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        
        if (triggerButtonDown)
        {
            Debug.Log("Trigger Button Down is pressed!");
            //ray.GetComponent<Renderer>().enabled = true;
        }
        if (triggerButtonUp)
        {
            Debug.Log("Trigger Button UP is pressed!");
            //ray.GetComponent<Renderer>().enabled = false;
        }
       
    }
}
