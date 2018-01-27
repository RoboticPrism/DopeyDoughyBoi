using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour {
    public int initialSegments = 4;


    private int speedScale = 80;
    private int rotationScale = 3;
    Rigidbody rb;
    public BodyController bodyControllerPrefab;
    List<BodyController> bodySegments = new List<BodyController>();
    ButtController buttSegment;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        buttSegment = FindObjectOfType<ButtController>();
        for (int i = 0; i < initialSegments; i++)
        {
            AddBody();
        }
        
	}
	
    void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector3(0, 0, Input.GetAxis("Vertical") * speedScale));
        rb.AddRelativeTorque(new Vector3(0, Input.GetAxis("Horizontal") * rotationScale, 0));
    }

	// Update is called once per frame
	void Update () {
		
	}

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
    }
}
