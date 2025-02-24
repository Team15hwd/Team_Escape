using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovingPlatform : TriggerController
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private LoopMode loopMode;
    [SerializeField] private StartMode startMode;
    [SerializeField] private LayerMask playerLayers;

    private Rigidbody2D rid;
    private BoxCollider2D col;

    private Transform wayPoint1;
    private Transform wayPoint2;

    public RaycastHit2D[] hits = new RaycastHit2D[4];

    public Vector2 velocity;
    public Vector2 direction;
    public Vector2 waypoint1Pos;
    public Vector2 waypoint2Pos;
    public Vector2 targetPos;

    private bool isRuning;

    void Awake()
    {
        rid = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        wayPoint1 = transform.GetChild(0).transform;
        wayPoint2 = transform.GetChild(1).transform;

        waypoint1Pos = wayPoint1.position;
        waypoint2Pos = wayPoint2.position;

        isRuning = startMode == StartMode.Run ? true : false;

        targetPos = waypoint1Pos;
    }

    void FixedUpdate()
    {
        if (!isRuning)
            return;

        var cast = Physics2D.BoxCastNonAlloc(rid.position, col.size * new Vector2(transform.localScale.x, transform.localScale.y) -new Vector2(0.1f, 0.1f), 
            0f, Vector2.up, hits, 0.1f, playerLayers);

        for (int i = 0; i < cast; i++)
        {
            //캐시로 최적화 합시다
            var cc = hits[i].transform.GetComponent<CharacterController2D>();

            if (Mathf.Sign(velocity.y) >= 0)
            {
                cc.externalVelocity = new Vector2(velocity.x, 0f);
            }
            else
            {
                cc.externalVelocity = velocity;
            }

            if (cc.inputVelocity.x != 0f)
            {
                cc.externalVelocity = new Vector2(0f, cc.externalVelocity.y);
            }
        }

        Moving();
    }

#if UNITY_EDITOR
    void Update()
    {
        wayPoint1.position = waypoint1Pos;
        wayPoint2.position = waypoint2Pos;
    }
#endif

    void Moving()
    {
        direction = targetPos - rid.position;

        velocity = direction.normalized * moveSpeed;
        rid.velocity = velocity;

        if (direction.magnitude < 0.1f)
        {
            targetPos = targetPos == waypoint2Pos ? waypoint1Pos : waypoint2Pos;
            direction = targetPos - rid.position;
        }
    }

    public override void TriggerEnter(CharacterController2D cc)
    {
        isRuning = true;
    }

    public override void TriggerExit(CharacterController2D cc)
    {

    }

    public enum LoopMode
    { 
        On,
        Off
    }

    public enum StartMode
    {
        Run,
        Wait
    }
}
