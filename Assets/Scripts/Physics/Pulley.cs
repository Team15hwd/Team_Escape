using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulley : MonoBehaviour
{
    public GameObject plat1;
    public GameObject plat2;
    
    public float power = 1f;//블럭이 내려가는 속도 조절

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.gameObject.CompareTag("plat1")&& plat1.transform.position.y > -5)
        {
            plat1.transform.Translate(Vector3.down * power * Time.deltaTime);
            plat2.transform.Translate(Vector3.up * power * Time.deltaTime);
            
        }

        if (this.gameObject.CompareTag("plat2") && plat2.transform.position.y > -5)
        {
            plat2.transform.Translate(Vector3.down * power * Time.deltaTime);
            plat1.transform.Translate(Vector3.up * power * Time.deltaTime);
        }

    }
}
    
