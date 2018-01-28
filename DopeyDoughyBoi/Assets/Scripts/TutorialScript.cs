using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    public float fadeSpeed = 1f;
    bool fadeTransition = false;
    bool fadedIn = false;

    public Text tutorialText;
    public HeadController headController;
    string moveText = "Use Arrow Keys or WASD to move about";
    public bool goalTextComplete = false;
    string goalText = "Wrap your body around objects to get larger";
    public bool flyTextComplete = false;
    string flyText = "Hold Space to wiggle upwards";
    public bool endTextComplete = false;
    string endText = "Suround more objects to complete the area";
    
    // Use this for initialization
    void Start () {
        headController = FindObjectOfType<HeadController>();
        tutorialText.color = new Color(0, 0, 0, 0);
        tutorialText.text = moveText;
        StartCoroutine(FadeInAndOut());
	}
	
	// Update is called once per frame
	void Update () {
		if (!fadeTransition &&
             !goalTextComplete &&
             (Input.GetAxis("Horizontal") > 0.5f || Input.GetAxis("Vertical") > 0.5f))
        {
            tutorialText.text = goalText;
            goalTextComplete = true;
            StartCoroutine(FadeInAndOut());
        } else if (!fadeTransition &&
            !flyTextComplete &&
            goalTextComplete &&
            headController.bodySegments.Count > 4)
        {
            tutorialText.text = flyText;
            flyTextComplete = true;
            StartCoroutine(FadeInAndOut());
        }
        else if (!fadeTransition &&
            !endTextComplete &&
            flyTextComplete &&
            Input.GetAxis("Jump") > 0.5f)
        {
            tutorialText.text = endText;
            endTextComplete = true;
            StartCoroutine(FadeInAndOut());
        }
    }

    IEnumerator FadeInAndOut()
    {
        float a = 0;
        fadeTransition = true;
        while (a < 1)
        {
            tutorialText.color = new Color(0, 0, 0, a);
            a += fadeSpeed * Time.deltaTime;
            yield return null;
        }
        tutorialText.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(2);
        while (a > 0)
        {
            tutorialText.color = new Color(0, 0, 0, a);
            a -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
        tutorialText.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(1);
        fadeTransition = false;
    }
}
