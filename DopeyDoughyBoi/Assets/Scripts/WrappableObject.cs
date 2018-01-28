using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappableObject : MonoBehaviour {

    public int segmentsAdded = 1;
    public Material blehMaterial;
    private List<Material> originalMaterials;
    public bool wrapped = false;
    List<WrapPoint> wrapPoints = new List<WrapPoint>();
    public HeadController dopeyHead;

    //Mood controller
    public MoodController mood;

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

		//Assigning dopey head
        dopeyHead = GameObject.Find("HeadSegment").GetComponent<HeadController>();

        //Assigning mood controller
        mood = GameObject.Find("AngryTrigger").GetComponent<MoodController>();

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

		//Changing boi to happy whenever he wraps
        dopeyHead.StartEmotionChange(HeadController.Emotions.HAPPY);
        Debug.Log(dopeyHead.currentEmotion);

        //Going back to neutral
        mood.StartCoroutine("ReturnNeutral");
    }
}
