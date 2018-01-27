using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapPoint : MonoBehaviour {

    public bool wrapped = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider col)
    {
        if(col.GetComponent<HeadController>() || col.GetComponent<BodyController>())
        {
            wrapped = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<HeadController>() || col.GetComponent<BodyController>())
        {
            wrapped = false;
        }
    }
}
