using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreuPlayerBounds : MonoBehaviour
{

    // keeps player inside of preset location
    void LateUpdate()
    {
        Vector3 ViewPos = transform.position;
        ViewPos.x = Mathf.Clamp(ViewPos.x, -16f, 16f );
        ViewPos.z = Mathf.Clamp(ViewPos.z, -22f, -4f);
        transform.position = ViewPos;
    }
}
