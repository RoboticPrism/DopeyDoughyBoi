using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappableObject : MonoBehaviour {

    public Material material;
    public bool wrapped = false;
    List<WrapPoint> wrapPoints = new List<WrapPoint>();

	// Use this for initialization
	void Start () {
        wrapPoints = new List<WrapPoint>(GetComponentsInChildren<WrapPoint>());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!wrapped)
        {
            bool allWrapped = true;
            foreach (WrapPoint wp in wrapPoints)
            {
                allWrapped = allWrapped && wp.wrapped;
            }
            wrapped = allWrapped;
            if (wrapped) {
                OnWrap();
            }
        }
	}

    void OnWrap()
    {
        GetComponent<Renderer>().material = material;
        FindObjectOfType<HeadController>().AddBody();
    }
}
