using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulley : MonoBehaviour
{
    public Rigidbody2D targetPlat; //속도를 적용할 대상 rigidbody2D

    //Y값을 정하는 오브젝트
    private Transform wayPoint;

    List<GameObject> _gameObject = new List<GameObject>();//일단 만들고 안쓰는중
    Rigidbody2D _rigidbody;


    private bool isMove = false;//move함수 호출 여부
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();//리지드바디 참조
        //자식 오브젝트 저장
        wayPoint = transform.GetChild(0).transform;
    }
    private void Update()
    {
        //하다 버린 것
        //wayPoint값과 같다/ 리지드바디 타입이 다이나믹/ MOVE함수 호출 false
        if (this.transform.position.y <= wayPoint.position.y && !_rigidbody.isKinematic && !isMove)
        {
            //속도를 초기화
            _rigidbody.velocity = Vector3.zero;
            //물리값을 받지않도록 설정
            _rigidbody.isKinematic = true;
            //
            _rigidbody.position = wayPoint.position;
        }

    }

    public void move(float speed)
    {
        Debug.Log(speed);
        //isMove = true;
        if (_rigidbody.isKinematic)
        {
            _rigidbody.isKinematic = false;
        }
        _rigidbody.velocity = Vector3.up * speed;
        //_rigidbody.AddForce(new Vector3(0, speed, 0));
    }


    //충돌된 오브젝트 저장
    //일단 만든 것
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _gameObject.Add(collision.gameObject);
        Debug.Log(_gameObject.Count);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _gameObject.Remove(collision.gameObject);
        Debug.Log(_gameObject.Count);
    }
}