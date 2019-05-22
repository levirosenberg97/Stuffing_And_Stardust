using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSphereRotater : MonoBehaviour
{
    public Vector3 rotation;
	void Start ()
    {
        StartCoroutine(Rotater());
	}
	
	
	IEnumerator Rotater()
    {
        while(true)
        {
            transform.Rotate(rotation);
            yield return null;
        }
    }
}
