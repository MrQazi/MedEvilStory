using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Player p;

    // Update is called once per frame
    void Update()
    {
        transform.position = p.transform.position;
    }
}
