using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    [SerializeField] //public과 같이 Unity 인스펙터에서 수정 가능하지만, private 처럼 다른 class에서 수정 불가능
    private float speed = 12f; //선언은 private으로 해야함

    [SerializeField]
    private float lifeTime = 3f;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private Rigidbody2D body;

    private float spawnTime = 0.0f;

    private void OnEnable() //오브젝트가 생성될 때 마다 호출
    {
        spawnTime = Time.time; //총알이 만들어지면 시간값을 저장
        body.linearVelocity = transform.right * speed;
    }

    void Update()
    {
        if (Time.time - spawnTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = collision.CompareTag("Player") == true;
        if (isPlayer == true)
        {
            /*
            PlayerRespawn pr = collision.GetComponent<PlayerRespawn>(); //충돌한 오브젝트의 컴포넌트중 <>에 해당하는 컴포넌트를 가져옴
            if (pr != null)
            {
                pr.Respawn();
            }

            //총알에 맞은 경우 바로 리스폰이 아닌 체력 감소를 적용시키기 위한 주석
            */

            PlayerHealth ph = collision.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.ApplyDamage();
            }

            Destroy(gameObject);
            return;
        }

        int otherLayer = collision.gameObject.layer; // 충돌 한 오브젝트의 레이어 정보를 가져옴
        bool isGround = ((groundMask.value & (1 << otherLayer)) != 0); // & → 논리연산, << → 시프트연산 
        if (isGround == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int otherLayer = collision.gameObject.layer; // 충돌 한 오브젝트의 레이어 정보를 가져옴
        bool isGround = ((groundMask.value & (1 << otherLayer)) != 0); // & → 논리연산, << → 시프트연산 
        if (isGround == true)
        {
            Destroy(gameObject);
        }
    }
}
