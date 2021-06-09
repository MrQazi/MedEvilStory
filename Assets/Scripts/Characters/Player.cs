using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public CharacterController controller;
    public Camera cam;

    public float speed = 6f;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    float drop = 0f;

    public bool isGrounded = false;
    public LayerMask Landable;

    public float Gravity = 2 * Physics.gravity.y;

    public Vector3 Movement = default;
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update() {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, Landable);
        if(isGrounded && drop < 0) {
            drop = -2f;
        }

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var dir = new Vector3(x, 0, z).normalized;

        if (dir.magnitude >= 0.1f) {
            var targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(Vector3.up * angle);

            var move = Quaternion.Euler(Vector3.up * targetAngle) * Vector3.forward;
            controller.Move(move.normalized * speed * Time.deltaTime);
        }
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            drop = Mathf.Sqrt(3f * -2f * Gravity);
        }
        drop += Gravity * Time.deltaTime;
        controller.Move(drop *Vector3.up * Time.deltaTime);
    }
}