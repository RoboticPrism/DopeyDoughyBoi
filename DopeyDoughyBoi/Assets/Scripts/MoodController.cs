using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodController : MonoBehaviour {

	//Defining the head controller
	public HeadController dopeyHead;

	// Use this for initialization
	void Start () {

		//Assigning dopey head
        dopeyHead = GameObject.Find("HeadSegment").GetComponent<HeadController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Collision detection
    void OnTriggerEnter (Collider col)
	{
		Debug.Log("Colliding");
		if (col.gameObject.GetComponent<WrappableObject> () != null) {
			Debug.Log("Colliding with wrap");
			Hormonal ();
		}
	}

    // Changing the emotion state of dough boi if they hit a wrappable object
    public void Hormonal ()
	{
		//Starts the change to angry after hitting
		dopeyHead.StartEmotionChange(HeadController.Emotions.ANGRY);
		Debug.Log(dopeyHead.currentEmotion);

		StartCoroutine("ReturnNeutral");
	}

	//For returning the emotional state to neutral
	IEnumerator ReturnNeutral ()
    {
    	yield return new WaitForSeconds(4);

		//Starts the change back to neutral so not permanently ANGERY
		dopeyHead.StartEmotionChange(HeadController.Emotions.NEUTRAL);
    }
}
