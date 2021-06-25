using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public NavMeshAgent agent;
    public Animator animator;
    public Fighter Owner;
    public LayerMask lookfor;
    public float PeripheralVision = 90f;
    public float SafeDistance = 2f;
    public Fighter target;
    float speed;
    public RaycastHit[] results = new RaycastHit[10];
    private void Start() {
        speed = agent.speed;
    }
    private void Update(){
        if (target) {
            if(Vector3.Distance(transform.position,target.transform.position) <= agent.stoppingDistance) {
                if (Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance/2) {
                    agent.SetDestination(target.transform.position + (transform.position-target.transform.position).normalized*SafeDistance);
                }
                else {
                    Owner.Attack(target.transform);
                }
            }
            else {
                agent.SetDestination(target.transform.position);
            }
        }
        else {
            SeeAround();
        }
        animator.SetFloat("Speed", agent.velocity.normalized.magnitude);
    }
    void SeeAround() {
        int l = Physics.SphereCastNonAlloc(transform.position, 15f, Vector3.forward, results, 1, lookfor);
        if (l > 0) {
            for (int i = 0; i < l; i++) {
                RaycastHit res = results[i];
                if (Mathf.Abs(Vector3.Angle(transform.forward, (res.transform.position - transform.position).normalized)) < PeripheralVision) {
                    target = res.collider.GetComponent<Fighter>();
                }
            }
        }
    }
}
