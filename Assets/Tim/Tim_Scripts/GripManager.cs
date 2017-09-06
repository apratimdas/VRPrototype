using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GripManager : MonoBehaviour {

    public Rigidbody body;
    public GameObject weapon;
    public GameObject model;
    public float groundHit;
    public Pull left;
    public Pull right;
    public bool isGrounded = true;
    bool doubleGripped;
    bool weaponselect=false;
    public Camera playerCamera;
    // Use this for initialization
    void Start () {
		
	}
    void FixedUpdate()
    {
        var ldevice = SteamVR_Controller.Input((int)left.controller.index);
        var rdevice = SteamVR_Controller.Input((int)right.controller.index);

        bool isGripped = left.canGrip || right.canGrip;
        doubleGripped = left.canGripAir && right.canGripAir;

        //if (doubleGripped)
        //    Debug.Log("Double pressed");

        if(ldevice.GetTouch(SteamVR_Controller.ButtonMask.ApplicationMenu) || rdevice.GetTouch(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if(rdevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            weaponselect = !weaponselect;
        }
        if(weaponselect)
        {
            model.SetActive(false);
            weapon.SetActive(true);
        }
        else
        {
            model.SetActive(true);
            weapon.SetActive(false);
        }

        //Debug.Log(isGrounded);
        //Debug.Log(body.transform.rotation);
        //Debug.Log(left.transform.localPosition);
        //Debug.Log(body.transform.rotation * left.transform.localPosition);


        if (doubleGripped && isGrounded)
        {
            body.useGravity = false;
            body.isKinematic = true;
            Vector3 pos = left.previousPos - left.transform.localPosition;
            Debug.Log("pos" + pos);
            Debug.Log("Rotation" + body.transform.rotation);
            Debug.Log("rotated pos" + body.transform.rotation * pos);

            body.transform.position += body.transform.rotation * pos;
            //body.velocity = Mathf.Max((left.previousPos - left.transform.localPosition) / Time.deltaTime , (right.previousPos - right.transform.localPosition)/Time.deltaTime);
            Vector3 lvelocity = body.transform.rotation * (left.previousPos - left.transform.localPosition) / Time.deltaTime;
            Vector3 rvelocity = body.transform.rotation * (right.previousPos - right.transform.localPosition) / Time.deltaTime;

            if(ldevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) || rdevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                body.velocity = Mathf.Max(lvelocity.magnitude, rvelocity.magnitude) == lvelocity.magnitude ? body.transform.rotation * lvelocity : body.transform.rotation * rvelocity;
                isGrounded = false;
            }
        }

        if (isGripped)
        {
            if (left.canGrip && (ldevice.GetTouch(SteamVR_Controller.ButtonMask.Grip) || ldevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger))) //Can Change control here
            {
                body.useGravity = false;
                body.isKinematic = true; //should be true
                body.transform.position += body.transform.rotation * (left.previousPos - left.transform.localPosition);
            }
            else if (left.canGrip && (ldevice.GetTouchUp(SteamVR_Controller.ButtonMask.Grip) || ldevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)))
            {
                body.useGravity = true;
                body.isKinematic = false; //should be false
                body.velocity = body.transform.rotation * (left.previousPos - left.transform.localPosition) / Time.deltaTime;
            }
            if (right.canGrip && (rdevice.GetTouch(SteamVR_Controller.ButtonMask.Grip) || rdevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger))) //Can Change control here
            {
                body.useGravity = false;
                body.isKinematic = true;
                body.transform.position += body.transform.rotation * (right.previousPos - right.transform.localPosition);
            }
            else if (right.canGrip && (rdevice.GetTouchUp(SteamVR_Controller.ButtonMask.Grip) || rdevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)))
            {
                body.useGravity = true;
                body.isKinematic = false;
                body.velocity = body.transform.rotation * (right.previousPos - right.transform.localPosition) / Time.deltaTime;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.tag == "Floor" && (!doubleGripped))
        //    isGrounded = false;
    }
}
