using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private void Update() {
        transform.eulerAngles = Camera.main.transform.eulerAngles.y * Vector3.up;
    }
}
