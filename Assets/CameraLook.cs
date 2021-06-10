using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform Target;
    public Transform Player;

    public Vector2 Sensitivity = default;
    public Vector2 Rot = default;
    // Update is called once per frame
    void Update()
    {
        Target.position = Player.position;

        var x = Input.GetAxis("Mouse X") * Sensitivity.x * Time.deltaTime;
        var y = Input.GetAxis("Mouse Y") * Sensitivity.y * Time.deltaTime;

        Rot.x += y;
        Rot.x = Mathf.Clamp(Rot.x, -90, 90);

        Rot.y += x;
        Target.localRotation = Quaternion.Euler(Rot.x, Rot.y, 0f);
    }
}
