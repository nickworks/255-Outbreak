using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for healthbars to carry a reference to the camera
/// </summary>
public class LookAtTarget : MonoBehaviour
{
    /// <summary>
    /// Reference to the main camera
    /// </summary>
    public Camera main_cam;

    Camera cam;

    // Use this for initialization
    void Start()
    {
        cam = main_cam;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
