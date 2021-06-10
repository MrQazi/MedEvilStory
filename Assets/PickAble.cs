using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAble : MonoBehaviour
{
    public Telekinesis owner;
    bool held;
    public ParticleSystem particles;
    public void PickUp(Telekinesis by) {
        owner = by;
        if (owner) {
            gameObject.layer = owner.gameObject.layer;
            GetComponent<Rigidbody>().isKinematic = true;
            Vector3 p = default;
            if (Vector3.Distance(transform.position, owner.transform.position) > 10f) {
                p = owner.transform.position + (transform.position-owner.transform.position).normalized * 10f;
            }
            else {
                p = transform.position;
            }
            p += (Vector3.up * owner.GetComponent<CharacterController>().height * 3);
            StartCoroutine(Lift(p));
        }
        else {
            GetComponent<Rigidbody>().isKinematic = false;
            gameObject.layer = 31;
            held = false;
        }
        GetComponent<MeshRenderer>().material.SetFloat("PickedUp", owner ? 1 : 0);
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == gameObject.layer || gameObject.layer == 31f || gameObject.layer == 3) {
            return;
        }
        Damage d = default;
        var rb = GetComponent<Rigidbody>();
        d.Attacker = owner.GetComponent<CharacterController>();
        d.value = rb.mass * rb.velocity.magnitude;
        collision.gameObject.SendMessage("TakeDamage", d,SendMessageOptions.DontRequireReceiver);
        Instantiate<ParticleSystem>(particles, collision.GetContact(0).point, Quaternion.Euler(collision.GetContact(0).normal)).gameObject.SetActive(true);
        owner.Drop();
    }
    public void ThrowAt(Vector3 pos) {
        held = false;
        transform.LookAt(pos);
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(transform.forward * 100 * rb.mass, ForceMode.Impulse);
    }
    public IEnumerator Lift(Vector3 pos) {
        held = true;
        while (Vector3.Distance(transform.position, pos) > 0.25 && held) {
            transform.position = Vector3.Slerp(transform.position, pos, Time.deltaTime * 3f);
            yield return null;
        }
        held = false;
    }
}
public struct Damage {
    public CharacterController Attacker;
    public float value;
}
