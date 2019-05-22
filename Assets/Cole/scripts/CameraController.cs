using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    CursorLockMode WantedMode;

    public Transform Lumen;

    bool Colliding = false;

    public GameObject Cam;

    float force = 100;

    public Vector3 targetPos;
    // Use this for initialization
    void Start ()
    {
        targetPos = new Vector3(Lumen.position.x, Lumen.position.y + 10, Lumen.position.z - 10);

       
    }

    // Update is called once per frame
    void Update()
    {
        //targetPos = new Vector3(Lumen.position.x, Lumen.position.y + 10, Lumen.position.z - 10);

        //Vector3 velocity = Vector3.zero;
        //Vector3 forward = Lumen.transform.forward * 10.0f;
        //Vector3 needPos = Lumen.transform.position - forward;

        //transform.position = Vector3.SmoothDamp(transform.position, needPos, ref velocity, 0.05f);

        //transform.LookAt(Lumen.transform);
        //transform.rotation = Lumen.transform.rotation;

        //Cam.transform.position = Vector3.Lerp(Cam.transform.position, targetPos, Time.deltaTime * 10);

        CursorState();
    }

  

    void CursorState()
    {

        Cursor.lockState = WantedMode;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible == false)
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
        }
    }



    void CamCollision(Collision Other)
    {
        if(Other.gameObject.tag == ("Room"))
        {
            Colliding = true;
            Vector3 dir = Other.contacts[0].point - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * force);
        }
        // else if( Collision )
        //{
        //    Colliding = false;
        //}

    }

    //private void LateUpdate()
    //{
    //    gameObject.transform.position = PlayerLocal.position + Quaternion.Euler(currentY, currentX, currentX) * new Vector3(0, camHeight, distance);   
    //}

}
