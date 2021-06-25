using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Player : MonoBehaviour {
    public UnityEvent<Enemy,bool> enemyUpdate;
    public CharacterController controller;
    public Fighter fighter;
    public NavMeshAgent agent;
    public Camera cam;

    public float speed = 6f;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Projectile p;

    float drop = 0f;

    public bool isGrounded = false;
    public LayerMask Landable;

    public float Gravity = 2 * Physics.gravity.y;
    public Animator animator;
    public Vector3 Movement = default;
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update() {
        isGrounded = Physics.CheckSphere(transform.position, controller.radius/2, Landable);
        if(isGrounded && drop < 0) {
            drop = -2f;
        }

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        animator.SetFloat("Speed",new Vector2(x,z).magnitude);

        var dir = new Vector3(x, 0, z).normalized;

        var targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(Vector3.up * angle);

        if (dir.magnitude>0.01f && fighter.free) {
            var move = Quaternion.Euler(Vector3.up * targetAngle) * Vector3.forward;
            controller.Move(move.normalized * speed * Time.deltaTime);
        }
        else {
            controller.Move(new Vector3()*0f);
        }
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            drop = Mathf.Sqrt(3f * -2f * Gravity);
        }
        drop += Gravity * Time.deltaTime;
        controller.Move(drop *Vector3.up * Time.deltaTime);

        switch (fighter.Weapon) {
            case WeaponType.Sword:
                if (Input.GetButtonDown("Fire1")) {
                    fighter.sword.Attack();
                }
                break;
            default:
                break;
        }

    }
     public void TakeDamage(Damage d) {
        enemyUpdate.Invoke(d.Attacker.GetComponent<Enemy>(), true);
    }

}
[Serializable]
public class PlayerData {
    public Vector3 position;

    public void Save() {
        var p = Manager.Inst.player;
        position = p.transform.position;
    }
    public void Load() {
        var p = Manager.Inst.player;
        p.transform.position = position;
        p.gameObject.SetActive(true);
    }
}
public enum WeaponType {
    None,
    Sword,
    Bow,
    Tele
}