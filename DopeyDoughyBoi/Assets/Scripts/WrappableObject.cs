﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappableObject : MonoBehaviour {

    public int segmentsAdded = 1;
    public Material blehMaterial;
    private List<Material> originalMaterials;
    public bool wrapped = false;
    List<WrapPoint> wrapPoints = new List<WrapPoint>();

	// Use this for initialization
	void Start () {
        wrapPoints = new List<WrapPoint>(GetComponentsInChildren<WrapPoint>());
        originalMaterials = new List<Material>(GetComponent<Renderer>().materials);
        List<Material> blehMaterials = new List<Material>();
        for(int i = 0; i< originalMaterials.Count; i++)
        {
            blehMaterials.Add(blehMaterial);
        }
        GetComponent<Renderer>().materials = blehMaterials.ToArray();
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
        GetComponent<Renderer>().materials = originalMaterials.ToArray();
        for (int i = 0; i < segmentsAdded; i++)
        {
            FindObjectOfType<HeadController>().AddBody();
        }
    }
}
