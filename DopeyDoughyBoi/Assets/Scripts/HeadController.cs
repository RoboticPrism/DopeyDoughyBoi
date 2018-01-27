using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour {
    public enum Emotions { NEUTRAL, HAPPY, SAD, ANGRY, FAST }
    public Emotions currentEmotion = Emotions.NEUTRAL;
    float emotionTransitionSpeed = 0.03f;

    public int initialSegments = 4;

    //music
    public int musicThreshold1 = 20;
    public int musicThreshold2 = 40;
    MusicController musicController;

    private int speedScale = 80;
    private int rotationScale = 3;
    private int climbScale = 150;
    Rigidbody rb;
    List<Renderer> renderers;
    public BodyController bodyControllerPrefab;
    List<BodyController> bodySegments = new List<BodyController>();
    ButtController buttSegment;

    public Material neutralMat;
    public Material happyMat;
    public Material sadMat;
    public Material angryMat;
    public Material fastMat;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
        buttSegment = FindObjectOfType<ButtController>();
        musicController = FindObjectOfType<MusicController>();
        for (int i = 0; i < initialSegments; i++)
        {
            AddBody();
        }
        StartEmotionChange(Emotions.NEUTRAL);
	}
	
    void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector3(0, 0, Input.GetAxis("Vertical") * speedScale + bodySegments.Count/5));
        rb.AddRelativeTorque(new Vector3(0, Input.GetAxis("Horizontal") * rotationScale, 0));
        if (buttSegment.onGround)
        {
            rb.AddForce(new Vector3(0, Input.GetAxis("Jump") * climbScale, 0));
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    //////////////
    // EMOTIONS //
    //////////////

    void StartEmotionChange(Emotions newEmotion)
    {
        StopCoroutine("ChangeEmotion");
        StartCoroutine("ChangeEmotion");
    }

    IEnumerator ChangeEmotion ()
    {
        int counter = 120;
        while(counter > 0)
        {
            UpdateMaterial();
            counter--;
            yield return null;
        }
    }

    void UpdateMaterial()
    {
        if(currentEmotion == Emotions.NEUTRAL)
        {
            LerpMaterial(neutralMat);
        } else if (currentEmotion == Emotions.HAPPY)
        {
            LerpMaterial(happyMat);
        }
        else if (currentEmotion == Emotions.SAD)
        {
            LerpMaterial(sadMat);
        }
        else if (currentEmotion == Emotions.ANGRY)
        {
            LerpMaterial(angryMat);
        }
        else if (currentEmotion == Emotions.FAST)
        {
            LerpMaterial(fastMat);
        }
    }

    void LerpMaterial(Material targetMat)
    {
        foreach (Renderer hRenderer in renderers)
        {
            hRenderer.material.Lerp(hRenderer.material, targetMat, emotionTransitionSpeed);
        }
        foreach (BodyController body in bodySegments)
        {
            foreach (Renderer bRenderer in body.renderers)
            {
                bRenderer.material.Lerp(bRenderer.material, targetMat, emotionTransitionSpeed);
            }
        }
        if (buttSegment.renderers != null)
        {
            foreach (Renderer bRenderer in buttSegment.renderers)
            {
                bRenderer.material.Lerp(bRenderer.material, targetMat, emotionTransitionSpeed);
            }
        }
    }

    /////////////////////
    // SEGMENT CONTROL //
    /////////////////////

    private BodyController LastBodySegment()
    {
        if (bodySegments.Count > 0)
        {
            return bodySegments[bodySegments.Count - 1];
        } else
        {
            return null;
        }
    }

    public void AddBody()
    {
        Transform parentSegment;
        if (bodySegments.Count > 0)
        {
            parentSegment = LastBodySegment().transform;
        } else
        {
            parentSegment = transform;
        }
        Vector3 spawnPoint = parentSegment.position;
        BodyController newBody = Instantiate(bodyControllerPrefab, this.transform.parent);
        newBody.parentTransform = parentSegment.transform;

        // Link segment to segment in front
        newBody.transform.position = spawnPoint;
        newBody.transform.Translate(Vector3.back, parentSegment);
        newBody.transform.rotation = parentSegment.rotation;
        newBody.GetComponent<CharacterJoint>().connectedBody = parentSegment.GetComponent<Rigidbody>();
        newBody.GetComponent<CharacterJoint>().connectedAnchor = new Vector3(0, 0, 0);
        bodySegments.Add(newBody);

        // move the butt further back
        buttSegment.transform.position = newBody.transform.position;
        buttSegment.transform.Translate(Vector3.back, newBody.transform);
        buttSegment.transform.rotation = newBody.transform.rotation;
        buttSegment.GetComponent<CharacterJoint>().connectedBody = newBody.transform.GetComponent<Rigidbody>();
        buttSegment.GetComponent<CharacterJoint>().connectedAnchor = new Vector3(0, 0, 0);

        if (bodySegments.Count == musicThreshold1)
        {
            musicController.ChooseMusic(1);
        } else if (bodySegments.Count == musicThreshold2)
        {
            musicController.ChooseMusic(2);
        }
        StartEmotionChange(currentEmotion);
    }
}
