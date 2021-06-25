using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RopeEnd : PickAble {
    public bool placed = false;
    public float place_speed = 2f;
    public Transform targetAnchor;
    public Transform airAnchor;
    public Obi.ObiRope rope;
    public Bridge bridge;

    public override void ThrowAt(Vector3 pos) {
        StartCoroutine(Place(pos));
    }
    public override void PickUp(Telekinesis by) {
        base.PickUp(by);
        if (placed) rb.isKinematic = true;
    }
    public IEnumerator Place(Vector3 pos) {
        StopCoroutine("Lift");
        bool g = false;
        while (Vector3.Distance(transform.position, pos) > 0.1f) {
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * place_speed);
            if(Vector3.Distance(targetAnchor.position, transform.position) < 0.5f) {
                transform.position = targetAnchor.position;
                transform.rotation = targetAnchor.rotation;
                placed = true;
                g = true;
                while(rope.stretchingScale > -0.5f) {
                    rope.stretchingScale -= 0.1f;
                    yield return null;
                }
                rope.transform.parent.GetComponent<Obi.ObiSolver>().collisionConstraintParameters.enabled = false;
                bridge.Fixed(true,this);
            }
            if (g) break;
            yield return null;
        }
    }
    override public IEnumerator Lift() {
        if (!placed) {

            held = true;
            var pos = airAnchor.position;
            while (Vector3.Distance(transform.position, pos) > 0.25 && held) {
                transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * Speed);
                transform.rotation = Quaternion.Lerp(targetAnchor.rotation, transform.rotation, Time.deltaTime * Speed);
                yield return null;
            }
            held = false;
        }
        else {
            owner.Drop();
        }
    }
}
