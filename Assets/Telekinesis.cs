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
    public Animator animator;
    public Fighter fighter;

    private void Update() {
        if (fighter.Weapon != WeaponType.Tele) return;
        if (pick) {
            animator.SetInteger("State", 3);
            if (Input.GetMouseButtonUp(1)) {
                Drop();
            }
            else {
                var cam = Camera.main.transform;
                if(Physics.Raycast(cam.position,cam.forward,out RaycastHit res, Range * 1.5f, ToAttack)) {
                    if (res.collider.GetComponent<CharacterController>())
                        CrossHair.color = Color.red;
                    else
                        CrossHair.color = Color.green;
                }
                else {
                    CrossHair.color = Color.white;
                    res.point = cam.position + (cam.forward * Range * 1.5f);
                }
                if (Input.GetMouseButtonDown(0)) {
                    pick.ThrowAt (res.point);
                    transform.LookAt(res.point, Vector3.up);
                    transform.localEulerAngles = Vector3.up * transform.localEulerAngles.y;
                    GetComponent<Animator>().Play("MagicAttack");
                }
            }
        }
        else {
            if (animator.GetInteger("State") == 3) {
                animator.SetInteger("State", 0);
            }
            CrossHair.color = Color.white;
            if (Input.GetMouseButtonDown(1)) {
                Pick();
            }
        }
    }
    void Pick() {
        var cam = Camera.main.transform;
        if (Physics.SphereCast(cam.position,0.5f, cam.forward, out RaycastHit res, Range, Pickable)) {
            if (res.collider.TryGetComponent<PickAble>(out PickAble p)) {
                p.PickUp(this);
                pick = p;
                transform.LookAt(p.transform.position, Vector3.up);
                transform.localEulerAngles = Vector3.up * transform.localEulerAngles.y;
            }
        }
    }
    public void Drop() {
        pick.PickUp(null);
        pick = null;
    }
}
