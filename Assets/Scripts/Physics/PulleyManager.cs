using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PulleyManager : MonoBehaviour
{
    public List<Pulley> pulleyList;

    //비교할 플랫폼
    public GameObject plat1;
    public GameObject plat2;
    //비교할 플랫폼을 참조할 리지드바디
    private Rigidbody2D rb1;
    private Rigidbody2D rb2;
    //속도가 변했을 때만 실행을 하기위해 이전속도값을 받을 변수 생성;
    private float prevSpeed1 = 0f;
    private float prevSpeed2 = 0f;
    void Start()
    {
        rb1 = plat1.GetComponent<Rigidbody2D>();
        rb2 = plat2.GetComponent<Rigidbody2D>();

        if (rb1 == null || rb2 == null)
        {
            Debug.Log("리지드바디를 불러오지못한 것이 있다");
        }

    }

    void FixedUpdate()
    {
        if (rb1 == null || rb2 == null) return;

        //오브젝트의 속도를 측정
        float speed1 = rb1.velocity.y;
        float speed2 = rb2.velocity.y;
        //속도가 같으면 실행하지 않음
        if (speed1 == prevSpeed1 && speed2 == prevSpeed2) return;
        //이전 속도 최신화
        prevSpeed1 = speed1;
        prevSpeed2 = speed2;
        //-------------------------버릴것
        Debug.Log(speed1 + "SPEED1");
        Debug.Log(speed2 + "SPEED2");

        if (speed1 < speed2)//1번 플랫폼이 더 빠르면
        {
            foreach (Pulley pulley in pulleyList)
            {
                if (pulley.gameObject.name == "plat02")
                {
                    pulley.move(Mathf.Abs(speed1));
                }
            }
        }
        else if (speed1 > speed2)//2번 플랫폼이 더 빠르면
        {
            foreach (Pulley pulley in pulleyList)
            {
                if (pulley.gameObject.name == "plat01")
                {
                    pulley.move(Mathf.Abs(speed2));
                }
            }
        }
        else
        {

        }
    }

    //반대 플랫폼을 올라가게 만들기
    //반대플랫폼의 속도를 반대로 주어 위로 올라감
    //경우1 한쪽만 위에 오브젝트가 올라간 경우
    //반대 플랫폼에 속도만 주면됨
    //경우2 양쪽다 오브젝트가 올라간 경우
    //서로 비교해서 속도값을 비교해서 속도를 조절해야함
    //a.velocity = 10    b.velocity = 5 이면 a-b를 하여 남은 값을 a b에 적용해야함
    //같은면 가만히있기

    //1.속도비교
    //2.값에 따라 플랫폼 이동
}
