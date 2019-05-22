using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed;
    public float JumpHeight;
    //public float rotateSpeed;

    public Animator Anim;

    private bool Jumping;

    //public Transform Target;

    public AudioSource AudioFile;

    public bool isPlayable;

    public float MouseSen = 100.0f;
    public float ClampAngle = 80.0f;

    private float RotY = 0.0f;
    private float RotX = 0.0f;
    private PlayerController player;


    // Use this for initialization
    void Start ()
    {
        //  AudioFile.Play(0);
        isPlayable = true;
        Anim = gameObject.GetComponent<Animator>();

        Vector3 Rot = transform.localRotation.eulerAngles;
        RotY = Rot.y;
        RotX = Rot.x;

        Cursor.visible = false;

        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        // transform.LookAt(Target);

        //float MoveHorizontal = Input.GetAxis("Horizontal");
        //float MoveVertical = Input.GetAxis("Vertical");
        //float Jumping = Input.GetAxis("Jump");

        //Vector3 Movement = new Vector3(MoveHorizontal, 0.0f, MoveVertical);

        //Vector3 Jump = new Vector3(0.0f, Jumping, 0.0f);
        if (isPlayable == true)
        {

            AudioControl();

            if (Input.GetKey("w")  || Input.GetKey("up") )
            {
                Anim.SetBool("IsWalking", true);
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                Debug.Log("Movement should be happening");
            }
            //AudioFile.Stop();

            if (Input.GetKey("s")  || Input.GetKey("down") )
            {
                Anim.SetBool("IsWalking", true);
                transform.Translate(-Vector3.forward * Speed * Time.deltaTime);
                if (Input.GetKeyDown("s"))
                {
                    AudioFile.Play();
                }

                if (Input.GetKeyUp("s")  || Input.GetKeyUp("down") ) 
                {
                    AudioFile.Pause();
                }
            }
            //AudioFile.Stop();

            if (Input.GetKey("a")  || Input.GetKey("left"))
            {
                Anim.SetBool("IsWalking", true);
                transform.Translate(Vector3.left * Speed * Time.deltaTime);
            }

            if (Input.GetKey("d")  || Input.GetKey("right") )
            {
                Anim.SetBool("IsWalking", true);
                transform.Translate(Vector3.right * Speed * Time.deltaTime);
            }

            Turning();

            //else
            //{
            //    AudioFile.Pause();
            //}

            //RB.AddForce(Jump * JumpHeight); 
        }
    }

    void AudioControl()
    {
     if (Input.GetButtonDown ("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            AudioFile.Play();
            
        }

     else if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical") && AudioFile.isPlaying)
        {
            AudioFile.Stop();
            Anim.SetBool("IsWalking", false);
        }

    }

    void Turning()
    {

        if (player != null && player.isPlayable == true)
        {

            float MouseX = Input.GetAxis("Mouse X");
            //float MouseY = Input.GetAxis("Mouse Y");

            RotY += MouseX * MouseSen * Time.deltaTime;
            //RotX += -MouseY * MouseSen * Time.deltaTime;

            RotX = Mathf.Clamp(RotX, -ClampAngle, ClampAngle);

            Quaternion localRotation = Quaternion.Euler(RotX, RotY, 0.0f);
            transform.rotation = localRotation;
        }

        else
        {
            Cursor.visible = true;
        }

    }

    //https://answers.unity.com/questions/890258/have-a-delay-after-each-jump-so-user-cant-spam-jum.html


}
