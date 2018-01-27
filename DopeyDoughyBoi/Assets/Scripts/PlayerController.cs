using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private int speedScale = 100;
    private int rotationScale = 5;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
    void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector3(0, 0, Input.GetAxis("Vertical") * speedScale));
        rb.AddRelativeTorque(new Vector3(0, Input.GetAxis("Horizontal") * rotationScale, 0));
    }

	// Update is called once per frame
	void Update () {
		
	}
}
