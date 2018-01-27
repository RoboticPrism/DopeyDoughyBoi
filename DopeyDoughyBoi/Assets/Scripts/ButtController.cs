using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtController : MonoBehaviour {

    public bool onGround = true;
    public Transform parentTransform;
    public List<Renderer> renderers;

    // Use this for initialization
    void Start()
    {
        renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
    }

    // Update is called once per frame
    void Update () {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                              transform.rotation.eulerAngles.y,
                                              0);
    }

    void OnTriggerStay(Collider col)
    {
        if(col.GetComponent<HeadController>() == null &&
           col.GetComponent<BodyController>() == null &&
           col.GetComponent<ButtController>() == null)
        {
            onGround = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<HeadController>() == null &&
            col.GetComponent<BodyController>() == null &&
            col.GetComponent<ButtController>() == null)
        {
            onGround = false;
        }
    }
}
