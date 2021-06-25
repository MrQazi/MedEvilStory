using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Chinchilla : PickAble {
    public Friend Owner;
    public bool Rogue;
    public Vector3 dest;
    public NavMeshAgent agent;

    private void Update() {
        if (Vector3.Distance(transform.position, agent.destination) <= 2 * agent.stoppingDistance) {
            if (Rogue) {
                var dir = UnityEngine.Random.insideUnitSphere * 2f;
                if (NavMesh.SamplePosition(transform.position + (transform.position-Manager.Inst.player.transform.position) + (dir * 25f), out NavMeshHit res, 10f, -1)) {
                    dest = res.position;
                    agent.SetDestination(dest);
                }
            }
        }
    }
    public override void ThrowAt(Vector3 pos) {
        StartCoroutine(Give(pos));
    }
    public override void PickUp(Telekinesis by) {
        base.PickUp(by);
        agent.enabled = false;
    }
    public IEnumerator Give(Vector3 pos) {
        held = true;
        while (Vector3.Distance(transform.position, pos) > 0.25 && held) {
            transform.position = Vector3.Slerp(transform.position, pos, Time.deltaTime * 3f);
            yield return null;
        }
        held = false;
        if (Manager.Inst.data._Stage == Stage.ArrowSearch) {
            if ((Manager.Inst as Forest).arrowSearch.minimission == Mini.ChinchillaEscape) {
                if (Vector3.Distance(transform.position, Owner.transform.position) < 2) {
                    (Manager.Inst as Forest).arrowSearch.Done(Mini.ChinchillaEscape);
                }
            }
        }
    }
}