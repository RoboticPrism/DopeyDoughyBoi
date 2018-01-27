using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtController : MonoBehaviour {

    public bool onGround = true;
    public Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider col)
    {
        if(col.GetComponent<HeadController>() == null &&
           col.GetComponent<BodyController>() == null &&
           col.GetComponent<ButtController>() == null)
        {
            onGround = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<HeadController>() == null &&
            col.GetComponent<BodyController>() == null &&
            col.GetComponent<ButtController>() == null)
        {
            onGround = false;
        }
    }
}
