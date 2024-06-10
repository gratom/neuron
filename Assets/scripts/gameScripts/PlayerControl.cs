using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private SpaceShipControl shipControl;
    [SerializeField] private Text textState;

    // Update is called once per frame
    private void Update()
    {
        int f = 0;
        int r = 0;
        if (Input.GetKey(KeyCode.W))
        {
            f += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            f -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            r -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            r += 1;
        }
        shipControl.ControlValues(f, r);

        if (Input.GetKey(KeyCode.Space))
        {
            shipControl.Shoot();
        }

        textState.text = "Money:" + shipControl.Money + "\n" +
                         "HP:" + shipControl.HP + "\n";
    }
}
