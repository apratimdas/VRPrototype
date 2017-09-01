using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepThroughPortal : MonoBehaviour {

    public GameObject otherPortal;
    public GameObject body;

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
            body.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 3;
        }
    }
}
