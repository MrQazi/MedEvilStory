using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAble : MonoBehaviour
{
    public Telekinesis owner;
    public float Speed = 3f;
    public bool held;
    public ParticleSystem particles;
    public Rigidbody rb;
    private void Start() {
        TryGetComponent<Rigidbody>(out rb);
    }
    public virtual void PickUp(Telekinesis by) {
        owner = by;
        if (owner) {
            if(rb) rb.isKinematic = true;
            StartCoroutine(Lift());
        }
        else {
            if(rb) rb.isKinematic = false;
            held = false;
        }
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
        if(particles)Instantiate<ParticleSystem>(particles, collision.GetContact(0).point, Quaternion.Euler(collision.GetContact(0).normal)).gameObject.SetActive(true);
        owner.Drop();
    }
    public virtual void ThrowAt(Vector3 pos) {
        held = false;
        transform.LookAt(pos);
        rb.isKinematic = false;
        rb.AddForce(transform.forward * 100 * rb.mass, ForceMode.Impulse);
    }
    virtual public IEnumerator Lift() {
        Vector3 pos = default;
        if (Vector3.Distance(transform.position, owner.transform.position) > 10f) {
            pos = owner.transform.position + (transform.position - owner.transform.position).normalized * 10f;
        }
        else {
            pos = transform.position;
        }
        pos += (Vector3.up * owner.GetComponent<CharacterController>().height * 3);
        held = true;
        while (Vector3.Distance(transform.position, pos) > 0.25 && held) {
            transform.position = Vector3.Slerp(transform.position, pos, Time.deltaTime * Speed);
            yield return null;
        }
        held = false;
    }
}
public struct Damage {
    public CharacterController Attacker;
    public float value;
}
