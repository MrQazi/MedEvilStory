using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bridge : MonoBehaviour
{
    public RopeEnd[] Ropes;
    public Collider[] Placeholders;
    public Plank[] Planks;
    int i = 0;

    public void Fixed(bool rope,PickAble item) {
        if (rope) {
            if(Ropes[0].placed && Ropes[1].placed) {
                Placeholders[0].gameObject.SetActive(true);
            }
        }
        else {
            Placeholders[i].gameObject.SetActive(false);
            i++;
            if (i < Placeholders.Length) {
                Placeholders[i].gameObject.SetActive(true);
            }
        }
    }
}