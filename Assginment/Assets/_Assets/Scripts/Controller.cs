using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Animator Ani;
    public Transform Cam;
    public CharacterController controller;

    public float speed = 6;
    public float smoothness = 0.1f;
    float turnvelo;

    /// Gravity ///
    
    private float gravity = -9.81f;
    public Transform Groundpos;
    public LayerMask GroundLayer;
    public bool isGrounded;
    Vector3 Velocity;

    void Update()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(hori, 0, ver).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Ani.SetBool("idle1", false);
            Ani.SetBool("walk1", true);
            float TargetAngles = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y ;
            float Angles = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngles, ref turnvelo, smoothness);
            transform.rotation = Quaternion.Euler(0f, Angles, 0f);
           
            Vector3 movdirection = Quaternion.Euler(0f, Angles, 0f) * Vector3.forward;
            controller.Move(movdirection * speed * Time.deltaTime);
        }
       else
        {
            Ani.SetBool("idle1", true);
            Ani.SetBool("walk1", false);
        }
       
      /// JUMPING ////
        isGrounded = Physics.CheckSphere(Groundpos.position, .5f, GroundLayer);
        Velocity.y += gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);

        if (isGrounded && Velocity.y <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Velocity.y = 4;
            }
        }
    }
}
