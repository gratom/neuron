using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceProbe : MonoBehaviour
{

    public float multiplier;
    public List<Rigidbody> rigs;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1))
        {
            rigs[0].AddRelativeForce(Vector3.up * multiplier * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            rigs[1].AddRelativeForce(Vector3.up * multiplier * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            rigs[2].AddRelativeForce(Vector3.up * multiplier * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            rigs[3].AddRelativeForce(Vector3.up * multiplier * Time.deltaTime);
        }
    }

}
