using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour {
    

    public SteamVR_TrackedObject controller;

    [HideInInspector]
    public Vector3 previousPos;

    public bool canGrip;
    public bool canGripAir;

	// Use this for initialization
	void Start () {
        previousPos = controller.transform.localPosition;
	}

    private void Update()
    {
        var device = SteamVR_Controller.Input((int)controller.index);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            canGripAir = true;
        else
            canGripAir = false;

    }

    //Update is called once per frame
    //void FixedUpdate () {
    //    var device = SteamVR_Controller.Input((int)controller.index);
    //    if(canGrip && device.GetTouch(SteamVR_Controller.ButtonMask.Grip)) //Can Change control here
    //    {
    //        body.useGravity = false;
    //        body.isKinematic = true;
    //        body.transform.position += (previousPos - controller.transform.localPosition);
    //    }
    //    else if(canGrip && device.GetTouchUp(SteamVR_Controller.ButtonMask.Grip))
    //    {
    //        body.useGravity = true;
    //        body.isKinematic = false;
    //        body.velocity = (previousPos - controller.transform.localPosition) / Time.deltaTime;
    //    }
    //    else
    //    {
    //        body.useGravity = true;
    //        body.isKinematic = false;
    //    }
    //    previousPos = controller.transform.localPosition;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GrabSurface")
            canGrip = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GrabSurface")
            canGrip = false;
    }
}
