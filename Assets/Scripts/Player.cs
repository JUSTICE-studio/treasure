using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviour
{
    public FixedTouchField Touchfield;
    public PhotonView PV;

    public Camera Cam;

    private float CameraAngleY;
    private float CameraAngleSpeed = 0.1f;
    private float CameraPosY;
    private float CameraPosSpeed = 0.1f;

    public float speed;

    public CharacterController controller;

    public float gravity = -9.81f;

    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpHeight = 3;

    Vector3 velocity;
    // Start is called before the first frame update
    void Awake()
    {
        /*if (gameObject.GetComponentInChildren<Camera>() != null)
            Cam = gameObject.GetComponentInChildren<Camera>();
        else Debug.LogError("Camera is null.");*/
        if (!PV.IsMine)
        {
            Cam.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        if (!PV.IsMine)
            return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
