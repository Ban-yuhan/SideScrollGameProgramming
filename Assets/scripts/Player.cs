using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 5.0f;

    public float JumpPower = 5.0f;
    public float groundCheckRadius = 0.12f;
    public Transform footPoint;
    public LayerMask groundMask;

    private float moveInput;


    public Rigidbody2D rb;
    private SpriteRenderer sr;



    private bool isGrounded = false;
    private bool wantsToJump = false;
    public float rayLength = 0.2f;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }



    void Update()
    {
        PlayerHead();
        //PlayerMove2();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();

        }

    }



    private void FixedUpdate()
    {

        PlayerMove();

    }



    void PlayerMove()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2((MoveSpeed * moveInput), rb.linearVelocity.y);

    }

    /*
    void PlayerMove2()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");

        Vector3 position = new Vector3(X, Y, 0);

        gameObject.transform.position = gameObject.transform.position + (position * MoveSpeed * Time.deltaTime);
    }
    */


    void PlayerHead()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            sr.flipX = true;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sr.flipX = false;
        }

    }



    void Jump()
    {
        if (footPoint != null)
        {
            Collider2D hit = Physics2D.OverlapCircle((Vector2)footPoint.position, groundCheckRadius, groundMask); //footpointposition 에서부터 groundCheckRadius의 반경의 원(OverlapCircle)에 groundMask에 해당하는 Layer의 오브젝트가 들어가있는지 체크해 정보가 있으면 hit변수에 들어가게됨

            if (hit != null)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }


        if (isGrounded == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0.0f);
            rb.AddForce(new Vector2(0.0f, JumpPower), ForceMode2D.Impulse); //AddForce : 특정 방향으로 힘을 가해줌
        }

        if (isGrounded == true)
        {
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse); // 두번째 파라미터 = 한 번만 힘을 가하겠다.

        }


    }


    
    private void OnDrawGizmosSelected()
    {
        if (footPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(footPoint.position, groundCheckRadius);
        }
    }
    

    



}
