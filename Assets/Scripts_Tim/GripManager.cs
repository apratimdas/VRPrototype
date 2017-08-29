using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripManager : MonoBehaviour {

    public Rigidbody body;

    public Pull left;
    public Pull right;



	// Use this for initialization
	void Start () {
		
	}
    void FixedUpdate()
    {
        var ldevice = SteamVR_Controller.Input((int)left.controller.index);
        var rdevice = SteamVR_Controller.Input((int)right.controller.index);

        bool isGripped = left.canGrip || right.canGrip;

        if(isGripped)
        {
            if (left.canGrip && ldevice.GetTouch(SteamVR_Controller.ButtonMask.Grip)) //Can Change control here
            {
                body.useGravity = false;
                body.isKinematic = true;
                body.transform.position += (left.previousPos - left.transform.localPosition);
            }
            else if (left.canGrip && ldevice.GetTouchUp(SteamVR_Controller.ButtonMask.Grip))
            {
                body.useGravity = true;
                body.isKinematic = false;
                body.velocity = (left.previousPos - left.transform.localPosition) / Time.deltaTime;
            }

            if (right.canGrip && rdevice.GetTouch(SteamVR_Controller.ButtonMask.Grip)) //Can Change control here
            {
                body.useGravity = false;
                body.isKinematic = true;
                body.transform.position += (right.previousPos - right.transform.localPosition);
            }
            else if (right.canGrip && rdevice.GetTouchUp(SteamVR_Controller.ButtonMask.Grip))
            {
                body.useGravity = true;
                body.isKinematic = false;
                body.velocity = (right.previousPos - right.transform.localPosition) / Time.deltaTime;
            }
        }
        else
        {
            body.useGravity = true;
            body.isKinematic = false;
        }


        

        left.previousPos = left.transform.localPosition;
        right.previousPos = right.transform.localPosition;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
