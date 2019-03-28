using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonARPlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private void Start() {
        moveSpeed = 5f;
        rotateSpeed = 50f;
    }

    void Update() {
        transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        transform.Rotate(0f, rotateSpeed * Input.GetAxis("Mouse X") * Time.deltaTime, 0f);
    }
}
