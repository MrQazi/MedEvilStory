using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Transform offset;

    private void Start() {
        offset = default;
        offset.position = transform.position - target.position;
        offset.eulerAngles = transform.eulerAngles - target.eulerAngles;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + offset.position;
        transform.eulerAngles = target.eulerAngles + offset.eulerAngles;
    }
}
