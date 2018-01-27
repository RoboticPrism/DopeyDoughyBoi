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

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        for(int i = 0; i < initialSegments; i++)
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
        newBody.transform.position = spawnPoint;
        newBody.transform.Translate(Vector3.back, parentSegment);
        newBody.GetComponent<CharacterJoint>().connectedBody = parentSegment.GetComponent<Rigidbody>();
        bodySegments.Add(newBody);
    }
}
