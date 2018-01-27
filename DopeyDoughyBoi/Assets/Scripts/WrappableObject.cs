using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappableObject : MonoBehaviour {

    public int segmentsAdded = 1;
    public Material blehMaterial;
    private Material originalMaterial;
    public bool wrapped = false;
    List<WrapPoint> wrapPoints = new List<WrapPoint>();

	// Use this for initialization
	void Start () {
        wrapPoints = new List<WrapPoint>(GetComponentsInChildren<WrapPoint>());
        originalMaterial = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = blehMaterial;
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
        GetComponent<Renderer>().material = originalMaterial;
        for (int i = 0; i < segmentsAdded; i++)
        {
            FindObjectOfType<HeadController>().AddBody();
        }
    }
}
