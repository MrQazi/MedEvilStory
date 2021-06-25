using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankPlaceholder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Plank>(out Plank p)) {
            p.StopAllCoroutines();
            p.transform.position = transform.position;
            p.transform.rotation = transform.rotation;
            p.placed = true;
            p.rb.isKinematic = true;
            p.bridge.Fixed(false,p);
        }
    }
}
