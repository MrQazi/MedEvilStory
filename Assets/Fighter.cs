using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fighter : MonoBehaviour {
    public UnityEvent<int> Message;
    public Animator animator;
    public WeaponType Weapon;
    public Sword sword;
    public bool free = false;

    public float Health;
    float max_Health;
    public Image HealthBar;
    public void CollisionState(int message) {
        Message.Invoke(message);
    }
    public void Root(int enable) {
        animator.applyRootMotion = enable == 1;
    }
    private void Update() {
    }

    internal void Attack(Transform t) {
        if (t) {
            transform.LookAt(t);
            transform.localEulerAngles = Vector3.up * transform.localEulerAngles.y;
        }
        animator.SetInteger("State", (int)Weapon);
        switch (Weapon) {
            case WeaponType.Sword:
                sword.Attack();
                break;
        }
    }
    public void TakeDamage(Damage d) {
        Health -= d.value;
        HealthBar.fillAmount = (Health / max_Health);
        if (Health <= 0f) {
            Destroy(gameObject);
        }
    }
    private void Start() {
        max_Health = Health;
        HealthBar.fillAmount = 1f;
    }
}