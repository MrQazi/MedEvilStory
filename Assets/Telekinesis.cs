using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Telekinesis : MonoBehaviour
{
    public LayerMask Pickable;
    public LayerMask ToAttack;
    public PickAble pick;
    public float Range;
    public Image CrossHair;
    private void Update() {
        if (pick) {
            if (Input.GetMouseButtonUp(1)) {
                Drop();
            }
            else {
                var cam = Camera.main.transform;
                if(Physics.Raycast(cam.position,cam.forward,out RaycastHit res, Range * 1.5f, ToAttack)) {
                    CrossHair.color = Color.red;
                }
                else {
                    CrossHair.color = Color.white;
                    res.point = cam.position + (cam.forward * Range * 1.5f);
                }
                if (Input.GetMouseButtonDown(0)) {
                    pick.ThrowAt (res.point);
                }
            }
        }
        else {
            CrossHair.color = Color.white;
            if (Input.GetMouseButtonDown(1)) {
                Pick();
            }
        }
    }
    void Pick() {
        var cam = Camera.main.transform;
        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit res, Range, Pickable)) {
            if (res.collider.TryGetComponent<PickAble>(out PickAble p)) {
                p.PickUp(this);
                pick = p;
            }
        }
    }
    public void Drop() {
        pick.PickUp(null);
        pick = null;
    }
}
