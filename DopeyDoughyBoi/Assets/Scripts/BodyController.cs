using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {

    public Transform parentTransform;
    public List<Renderer> renderers;

    // Use this for initialization
    void Start()
    {
        renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
    }

    void FixedUpdate ()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                              transform.rotation.eulerAngles.y,
                                              0);
    }
}
