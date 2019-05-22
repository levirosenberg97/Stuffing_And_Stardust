using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemrotator : MonoBehaviour
{

    [Range(-200, 200)]
    public float XSpeed;
    [Range(-200, 200)]
    public float YSpeed;
    [Range(-200, 200)]
    public float ZSpeed;
    Vector3 Rotation;

    [Range(-1, 1)]
    public float amplitude;
    [Range(-10, 10)]
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        Rotation.x = XSpeed;
        Rotation.y = YSpeed;
        Rotation.z = ZSpeed;

        Rotation *= Time.deltaTime;

        transform.Rotate(Rotation);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
