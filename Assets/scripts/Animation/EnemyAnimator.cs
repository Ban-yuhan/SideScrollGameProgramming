using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAnimator : MonoBehaviour
{
    [Header("필수 참조")]
    //상태 전이를 제어할 Animator. Idle/Move/Jump 상태가 들어 있는 Controller가 연결되어야 한다.
    [SerializeField] private Animator animator;

    //이동속도를 읽을 Rigidbody2D.
    [SerializeField] private Rigidbody2D rb;

    //좌우 반전을 적용할 SpriteRenderer.
    [SerializeField] private SpriteRenderer spriteRenderer;

    //플립 전환이 너무 자주 튀는 것을 방지하는 최소 속도. 이 값 미만이면 플립을 보류.
    [SerializeField] private float flipDeadZone = 0.02f;

    //적이 플레이어를 탐지할 센서 위치
    [SerializeField] private Transform sensorPoint;



    //플레이어의 위치
    [SerializeField] private Transform player;
    
    //장애물이 사이에 있는지 검사
    [SerializeField] private LayerMask groundMask;

    //감지 범위
    [SerializeField] private  float detectRadius = 6.0f;

    //시야각
    [SerializeField] private float fovAngle = 90.0f;



    private PlayerHealth playerHealth;
    

    [SerializeField] private string paramCanSeePlayer = "CanSeePlayer";

    private bool canSeePlayer = false;


    private void Awake()
    {
        //Animator 캐시.
        Animator a = animator;

        if (a == null)
        {
            a = GetComponentInChildren<Animator>();
        }
        animator = a;

        // Rigidbody2D 캐시.
        Rigidbody2D r = rb;

        if (r == null)
        {
            r = GetComponent<Rigidbody2D>();
        }
        rb = r;

        //SpriteRenderer 캐시.
        SpriteRenderer sr = spriteRenderer;

        if (sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }

        spriteRenderer = sr;

        if (player == null)
            { 

            GameObject objectPlayer = GameObject.FindGameObjectWithTag("Player");

            if (objectPlayer != null)
            {
                player = objectPlayer.transform;
            }
        }

        if (sensorPoint == null)
        { 
        canSeePlayer = false;
        }

        //GameObject Player = GameObject.FindGameObjectWithTag("Player");
        //if (Player != null)
        //{
        //    playerHealth = player.GetComponent<PlayerHealth>();
        //}
    }
    private void Start()
    {
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();

        }
    }
    private void Update()
    {
        CanSeePlayer();
        Debug.Log("csp :" + canSeePlayer);

        Vector2 v = rb != null ? rb.linearVelocity : Vector2.zero;

        //수평 속도.
        float speedX = v.x;

        UpdateFlipX(speedX);

        ApplyAnimatorParams(canSeePlayer);

    }

    private void ApplyAnimatorParams(bool csp)
    {
        if (animator == null)
        {
            return;
        }

        animator.SetBool(paramCanSeePlayer, csp);

        
    }


    private void CanSeePlayer()
    {
        if (player == null || sensorPoint == null)
        {
            canSeePlayer = false;
            return;
        }

        bool isAlive = playerHealth.isAlive();

        Vector2 origin = sensorPoint.position;
        Vector2 toPlayer = (Vector2)(player.position - sensorPoint.position);
        float distance = toPlayer.magnitude; // 벡터의 크기(=거리) 계산 

        if (distance > detectRadius)
        {
            canSeePlayer = false;
            return;
        }

        Vector2 forward = sensorPoint.right; //광선을 쏠 방향
        float angle = Vector2.Angle(forward, toPlayer); // 플레이어가 있는 위치 사이의 각도

        if (angle > (fovAngle * 0.5f))
        {
            canSeePlayer = false;
            return;
        }
        



        RaycastHit2D block = Physics2D.Raycast(origin, toPlayer.normalized, distance, groundMask);
        bool blocked = (block.collider != null);
        if (blocked == true)
        {
            canSeePlayer = false;
            return;
        }

        if (isAlive == false)
        {
            canSeePlayer = false;
            return;
        }

        canSeePlayer = true;

    }

    /*
    private void EnterChase()
    {
        isChasing = true;
        notSeenTimer = 0.0f;

        if (patrol != null)
        {
            patrol.enabled = false;
        }

    }
    */

    private void UpdateFlipX(float speedX)
    {
        if (spriteRenderer == null)
        {
            return;
        }

        //오른쪽으로 이동하면 flipX = false, 왼쪽 이동이면 flipX = true.
        if (speedX > 0.0f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }



    //private void AlignSensorToDirection(int dir)
    //{
    //    Vector3 p = sensorPoint.localPosition;
    //    p.x = Mathf.Abs(p.x) * (dir > 0 ? 1.0f : -1.0f);
    //    sensorPoint.localPosition = p;

    //    Vector3 e = sensorPoint.localEulerAngles;
    //    e.y = (dir > 0) ? 0.0f : 108.0f;
    //    sensorPoint.localEulerAngles = e;
    //}
}
