using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour {

    HeadController headController;
    public int winLimit = 60;
    public string nextScene;
    bool swapping = false;
	// Use this for initialization
	void Start () {
        headController = FindObjectOfType<HeadController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(headController.bodySegments.Count > winLimit)
        {
            if(nextScene != null && !swapping) {
                swapping = true;
                SceneManager.LoadScene(nextScene);
            }
        }
	}
}
