using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 200f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate (Vector3.forward * thrustSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate (Vector3.down * thrustSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate (Vector3.up * thrustSpeed * Time.deltaTime);
        }

    }
}
