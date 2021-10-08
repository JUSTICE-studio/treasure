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

    Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        /*if (gameObject.GetComponentInChildren<Camera>() != null)
            Cam = gameObject.GetComponentInChildren<Camera>();
        else Debug.LogError("Camera is null.");*/
        if(!PV.IsMine)
        {
            Cam.gameObject.SetActive(false);
        }
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(h * speed, rb.velocity.y, v * speed);

        CameraPosY = Mathf.Clamp(CameraPosY + Touchfield.TouchDist.y * CameraPosSpeed * .1f, 0, 8f);

        CameraAngleY += Touchfield.TouchDist.x * CameraAngleSpeed;

        Cam.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, CameraPosY, 4);
        Cam.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Cam.transform.position, Vector3.up);
    }
}
