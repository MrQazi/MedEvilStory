using System.Collections;
using UnityEngine;

public class Plank : PickAble {
    public bool placed = false;
    public float place_speed = 2f;
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
        held = true;
        while (Vector3.Distance(transform.position, pos) > 0.01f && held) {
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * place_speed);
            yield return null;
        }
        held = false;
    }
}