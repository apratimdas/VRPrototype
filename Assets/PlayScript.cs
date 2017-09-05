using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour {

    private bool touched = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (touched)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), 1f);
        Debug.Log("Entered collision");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), 1f);
            Debug.Log("Entered trigger");
            GetComponent<Animator>().Play("PlayAnimation");

            SceneManager.LoadScene("PortalTest");

        }
    }
}
