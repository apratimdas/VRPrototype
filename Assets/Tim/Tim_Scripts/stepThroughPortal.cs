using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepThroughPortal : MonoBehaviour {

    public GameObject otherPortal;
    public GameObject body;
    public GameObject eyes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            Debug.Log("hit!");

            Vector3 bodyRotation = body.transform.eulerAngles;
            Vector3 eyeRotation = eyes.transform.eulerAngles;

            float angle = Vector3.Angle(eyes.transform.forward, transform.forward);

            Vector3 angleDiff = body.transform.eulerAngles - transform.eulerAngles;

            Quaternion portalRotator = Quaternion.FromToRotation(transform.forward, otherPortal.transform.forward);

            Quaternion bodyRotator = Quaternion.FromToRotation(body.transform.forward, otherPortal.transform.forward);

            Quaternion rotator = Quaternion.FromToRotation(eyes.transform.forward, otherPortal.transform.forward);

            body.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 2;
            Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

            body.transform.forward = portalRotator * body.transform.forward * -1;

            
            //body.transform.forward = forward.normalized * -1;
           
            
            //body.transform.forward = rotator * body.transform.forward;

            Vector3 speed = body.GetComponent<Rigidbody>().velocity;
            //float angletorotate = eyes.transform.eulerAngles.y - body.transform.eulerAngles.y;
            body.GetComponent<Rigidbody>().velocity = otherPortal.transform.forward.normalized * speed.magnitude;
            
            
            //body.transform.eulerAngles += new Vector3(0, body.transform.eulerAngles.y + angletorotate, 0);


            //body.transform.eulerAngles += angleDiff;
            
            //body.GetComponent<Rigidbody>().velocity = body.transform.rotation * body.GetComponent<Rigidbody>().velocity;
        }
    }
}
