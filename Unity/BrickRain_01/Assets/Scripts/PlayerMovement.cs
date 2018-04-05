using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float RotateSpeed = 50f;
   
    void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime, Space.World);
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.UpArrow))
            transform.Rotate(Vector3.right * RotateSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.DownArrow))
            transform.Rotate(-Vector3.right * RotateSpeed * Time.deltaTime);

	}

}
