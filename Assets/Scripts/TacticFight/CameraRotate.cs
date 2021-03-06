﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public void RotateLeft()
    {
        transform.Rotate(Vector3.up,90,Space.Self);
    }

    public void RotateRight()
    {
        transform.Rotate(Vector3.up, -90, Space.Self);
    }

}
