using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveScript : MonoBehaviour {

    Rigidbody rb;
    public float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update () {
        rb.velocity = new Vector3(Input.GetAxis("Vertical") * speed, 0, Input.GetAxis("Horizontal") * speed);
	}
}
