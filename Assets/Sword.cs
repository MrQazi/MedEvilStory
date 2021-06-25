using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Fighter Owner;
    public Collider Trigger;
    public float damage = 1f;

    public void Equip(Fighter owner) {
        Owner = owner;
        Owner.Message.RemoveAllListeners();
        Owner.Message.AddListener((message) => {
            switch (message) {
                case 0:
                    Trigger.enabled = false;
                    break;
                case 1:
                    Trigger.enabled = true;
                    Owner.animator.SetBool("Attack", false);
                    break;
                case 2:
                    Owner.animator.SetBool("Attack", false);
                    break;
            }
        });
    }
    private void Start() {
        Equip(Owner);
    }
    public void Attack() {
        if(!Owner.animator.GetBool("Attack")) {
            Owner.animator.SetBool("Attack", true);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Fighter>(out Fighter f)) {
            Damage d = default;
            d.Attacker = Owner.GetComponent<CharacterController>();
            d.value = damage;
            f.SendMessage("TakeDamage", d, SendMessageOptions.DontRequireReceiver);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Owner) {

        }
    }
}
