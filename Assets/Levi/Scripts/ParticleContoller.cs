using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleContoller : MonoBehaviour
{
    ParticleSystem particle;
    Vector3 startLocalPos;

    Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        StopParticle();
        startPos = transform.localPosition;
        startLocalPos = transform.localPosition;
    }

    public void StartParticle()
    {
        ResetPosition();
        particle.Play();
    }

    public void StopParticle()
    {
        particle.Stop();
    }

    public void MoveObject(float distance)
    {
        StartCoroutine(TranslateObject(distance));
    }

    public void ResetPosition()
    {
        transform.localPosition = startLocalPos;
    }

    IEnumerator TranslateObject(float newZ)
    {
        Vector3 newPos = new Vector3(startPos.x, startPos.y, startPos.z + newZ);
        Vector3 currentPos = startPos;
        while(transform.localPosition.z != newPos.z)
        {
            currentPos.z += Mathf.RoundToInt(newZ / 6);
            transform.localPosition = currentPos;

            if(transform.localPosition.z >= newPos.z)
            {
                StopCoroutine("TranslateObject");

                break;
            }
            yield return new WaitForSeconds(.02f);
        }
    }
}
