using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject Pulley;

    void FixedUpdate()
    {
        if(transform.position.y > Pulley.transform.position.y)
        {
            gameObject.SetActive(false);
        }

        if (transform.position.y <= Pulley.transform.position.y)
        {
            gameObject.SetActive(true);
        }

    }
}
