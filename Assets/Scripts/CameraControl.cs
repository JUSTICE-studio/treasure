using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Cam;
    public FixedTouchField FixedTouchField;
    private void Update()
    {
        print(FixedTouchField.TouchDist.y);
        if (Cam.transform.localPosition.y <= 1 && FixedTouchField.TouchDist.y > 0 || Cam.transform.localPosition.y >= 10 && FixedTouchField.TouchDist.y < 0)
        {
            Cam.transform.localPosition += new Vector3(0, FixedTouchField.TouchDist.y / 5f, 0);
            Cam.transform.Rotate(FixedTouchField.TouchDist.y, 0, 0);
        }

        //Cam.transform.y가 3이상이면 다시 앞으로 땡기기
    }
}
