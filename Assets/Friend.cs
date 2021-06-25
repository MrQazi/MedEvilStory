using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Friend : MonoBehaviour
{
    public FriendName Name;
    public Player player;
    public Fighter Owner;
    public NavMeshAgent agent;
    public Animator animator;
    public Enemy target;

    public float SafeDistance;
    public Vector3 lastDest;
    

    public float speed;
    private void Start() {
        speed = agent.speed;
        player.enemyUpdate.AddListener((enemy, critical) => {
            if (critical == false) return;
            target = enemy;
        }); ;
    }
    private void Update() {
        if (target) {
            if (Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance) {
                if (Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance / 2) {
                    FollowEnemy();
                    agent.SetDestination(target.transform.position + (transform.position - target.transform.position).normalized * SafeDistance);
                }
                else {
                    Owner.Attack(target.transform);
                }
            }
            else {
                FollowEnemy();
            }
        }
        else {
            if (Vector3.Distance(lastDest, player.transform.position) > 5f) {
                FollowPlayer();
            }
            else {

                if (Vector3.Distance(transform.position, player.transform.position) <= 10) {
                }
                else {
                    FollowPlayer();
                }
            }
        }
        
        animator.SetFloat("Speed", agent.velocity.normalized.magnitude);
    }
    void FollowPlayer() {
        var dir = UnityEngine.Random.insideUnitSphere * 3;
        
        if (NavMesh.SamplePosition(player.transform.position + dir, out NavMeshHit res, 5f,-1)) {
            agent.SetDestination(res.position);
            lastDest = res.position;
        }

    }
    void FollowEnemy() {
        agent.SetDestination(target.transform.position);
    }
}
[Serializable]
public class FriendData {
    public FriendName Name;
    public Vector3 position;

    public void Save(Friend f) {
        Name = f.Name;
        position = f.transform.position;
    }
    public void Load(Friend f) {
        f.Name = Name;
        f.transform.position = position;
    }
    public static void SaveAll() {
        foreach (Friend f in Manager.Inst.Friends) {
            if(Game.Inst.friends[(int)f.Name]==null) Game.Inst.friends[(int)f.Name] = new FriendData();
            Game.Inst.friends[(int)f.Name].Save(f);
        }
    }
    public static void LoadAll() {
        foreach (Friend f in Manager.Inst.Friends) {
            if (Game.Inst.friends[(int)f.Name] == null) Game.Inst.friends[(int)f.Name] = new FriendData();
            Game.Inst.friends[(int)f.Name].Load(f);
        }
    }
}
public enum FriendName {
    Leo,
    Helena,
    Jerry,
    Timmy,
    Charles,
    Diana
}
