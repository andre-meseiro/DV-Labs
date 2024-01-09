using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    // camera
    [SerializeField] private Transform cam;

    [SerializeField] private float speed = 6f;

    [SerializeField] private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        // lock the cursor in place so it doesn't move and also make it invisible (to make it appear again, i just need to press escape)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // moving
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {   
            // the Atan2 function calculates the angle to rotate the character in rad, that's why we need to use Rad2Deg to get the angle in degrees
            // also add the rotation of the camera on the Y axis, so the character can move in the camera direction (continues on the 2 last lines of the code)
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            // the SmoothDampAngle is used to smooth the angle
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // rotate the character itself
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // turn the rotation into a direction by multiplying by Vector3.forward, so the character can move in the camera direction
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            // normalize because it's related to moving
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }
}
