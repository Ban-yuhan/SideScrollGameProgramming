using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPatrol2D : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public int startdirectioni = 1; // 1: 오른쪽, -1 : 왼쪽. 맨 처음 방향정보를 저장.

    public Transform sensorPoint;
    public float wallCheckDistance = 0.25f;
    public float ledgeCheckDistance = 0.35f;
    public LayerMask groundMask; //지면 레이어만 추출

    public bool useFlipx = true;
    public Rigidbody2D body;
    public SpriteRenderer sprite;

    private int direction = 1; //바뀌는 방향은 여기에 입력.


    public EnemyState state;


    private void Awake()
    {
        direction = startdirectioni >= 0 ? 1 : -1; // 3항 연산 → startdirection이 0보다 크거나 같은지 비교, 같으면 1, 같지 않으면 -1을 집어넣음 
        /*풀어 쓰면 다음과 같음
         
        if(startDirection >= 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        
         */

        if (sensorPoint != null)
        {
            AlignSensorToDirection(direction);
        }

        ApplyVIsualFacing();
    }

    private void FixedUpdate()
    {
        if (state != null && state.GetAlive() == false)
        { 
            body.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 v = body.linearVelocity;
        v.x = direction * moveSpeed;
        body.linearVelocity = v;

        Vector2 origin = sensorPoint.position;
        Vector2 dirForward = sensorPoint.right;
        Vector2 dirDown = (Vector2)(-sensorPoint.up); //down이 없기때문에 up에 -를 붙여 사용


        RaycastHit2D hitWall = Physics2D.Raycast(origin, dirForward, wallCheckDistance, groundMask);
        bool hasWall = (hitWall.collider != null); //광선에 맞은 지면 오브젝트가 있다면


        RaycastHit2D hitDown = Physics2D.Raycast(origin, dirDown, ledgeCheckDistance, groundMask);
        bool hasGround = (hitDown.collider != null); //광선에 맞은 지면 오브젝트가 있다면
        if (hasWall == true || hasGround == false)
        {
            TurnAround();
        }
    }



    void Update()
    {
        
    }

    public void TurnAround()
    {
        direction *= -1;

        if (sensorPoint != null && useFlipx == true)
        {
            Vector3 p = sensorPoint.localPosition;
            p.x = p.x * -1.0f;
            sensorPoint.localPosition = p;

            Vector3 e = sensorPoint.localEulerAngles;
            e.y = (e.y + 180.0f) % 360.0f;
            sensorPoint.localEulerAngles = e;
        }

        ApplyVIsualFacing();
    }

    void AlignSensorToDirection(int dir)
    {
        Vector3 p = sensorPoint.localPosition;
        p.x = Mathf.Abs(p.x) * (dir > 0 ? 1.0f : -1.0f);
        sensorPoint.localPosition = p;

        Vector3 e = sensorPoint.localEulerAngles;
        e.y = (dir > 0) ? 0.0f : 108.0f;
        sensorPoint.localEulerAngles = e;
    }

    // 캐릭터의 방향을 전환하는 함수
    private void ApplyVIsualFacing()
    {
        if (useFlipx == true)
        {
            bool facingLeft = (direction > 0);
            sprite.flipX = facingLeft;
        }
        else 
        {
            Vector3 s = transform.localScale;

            s.x = Mathf.Abs(s.x) * (direction > 0 ? 1.0f : -1.0f); //Mathf : 수학 함수를 가져와주는 구조체. 작성시 유니티가 제공하는 수학 관련 함수 사용 가능. 
            //Mathf.Abs : 절대값을 반환해주는 함수
            // s.x = -1 * (-1 > 0 ? 1.0f : -1.0f); → s.x = -1 * -1 = 1 왼쪽을 보고있지만, 값은 오른쪽이 되어버림. 따라서 절대값을 사용해 올바른 방향전환을 할 수 있게 함
            transform.localScale = s;
        }
    }


    // 시야각과 센서의 방향만 바로잡아주기 위한 함수
    public void Face(int dir)
    {
        if (useFlipx == true)
        {
            if (sprite != null)
            {
                bool facingLeft = (dir > 0);
                sprite.flipX = facingLeft;
            }
        }
        else
        {
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x)*(dir > 0 ? 1.0f : -1.0f);
            transform.localScale = s;
        }

        if (sensorPoint != null)
        {
            Vector3 p = sensorPoint.localPosition;
            p.x = Mathf.Abs(p.x) * (dir > 0 ? 1.0f : -1.0f);
            sensorPoint.localPosition = p;

            Vector3 e = sensorPoint.localEulerAngles;
            e.y = (dir > 0) ? 0.0f : 180.0f;
            sensorPoint.localEulerAngles = e;
        }
    }

}
