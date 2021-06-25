using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCalculator : MonoBehaviour
{
    public Vector3 pos;
    public float BlendSpeed;
    public float Speed = 1000f;
    private void Start() {
        pos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        var v = transform.position - pos;
        pos = transform.position;
        var s = v.magnitude * Speed;
        BlendSpeed = s >= 1 ? 1 : s;
        GetComponent<Animator>().SetFloat("Speed", BlendSpeed);
    }
}
