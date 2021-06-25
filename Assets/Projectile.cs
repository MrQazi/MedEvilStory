using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public Vector3 Target;
    public float Angle = 45.0f;
    public float gravity = 9.8f;

    void OnEnable() {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot() {
        float D = Vector3.Distance(transform.position, Target);
        float V = D / (Mathf.Sin(2 * Angle * Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(V) * Mathf.Cos(Angle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(V) * Mathf.Sin(Angle * Mathf.Deg2Rad);

        float flightDuration = D / Vx;

        transform.rotation = Quaternion.LookRotation(Target - transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration) {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
