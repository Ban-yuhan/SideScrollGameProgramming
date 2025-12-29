using UnityEngine;

public class EnemySight2D : MonoBehaviour
{
    public Transform player;
    public Transform sensorPoint;
    public LayerMask groundMask; // 플레이어와 자신 사이에 장애물이 있는지 검사
    public float detectRadius = 6.0f; //감지 범위
    public float fovAngle = 90.0f; //시야각도

    private PlayerHealth playerHealth;

    

    void Start()
    {
        /*
        GameObject objectPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objectPlayer != null)
        { 
            player = objectPlayer.transform;
        }

        또는 아래와 같이 작성
        */
        GameObject objectPlayer = GameObject.Find("Player");
        if (objectPlayer != null) // 오브젝트를 찾고나서 null인지 아닌지 체크해주는게 좋음
        {
            player = objectPlayer.transform;
            playerHealth = player.GetComponent<PlayerHealth>();

        }

        /*
        Player playerMove = GameObject.FindAnyObjectByType<Player>();
        if (playerMove != null)
        {
        player = playerMove.transform;    
        }
        */
    }

    
    public bool CanSeePlayer()
    {
        if (player == null || sensorPoint == null)
        { 
            return false;
        }

        bool isAlive = playerHealth.isAlive();
        
        Vector2 origin = sensorPoint.position;
        Vector2 toPlayer = (Vector2)(player.position - sensorPoint.position); //position이 Vector3 형이기 때문에 Vector2 형으로 바꿔줘야함 → 형 변환 || 상호간에 변환이 가능한 형 끼리만 변환 가능. ex) int, float 형을 Vector2 형, Vector3 형 으로 변환할 수 없음.
        float distance =  toPlayer.magnitude; // 벡터의 크기(=거리) 계산 

        if (distance > detectRadius)
        { 
            return false ;  
        }

        Vector2 forward = sensorPoint.right; //광선을 쏠 방향
        float angle = Vector2.Angle(forward, toPlayer); // 플레이어가 있는 위치 사이의 각도
        
        if (angle > (fovAngle * 0.5f))
        { 
            return false;
        }


        RaycastHit2D block = Physics2D.Raycast(origin, toPlayer.normalized, distance, groundMask); 
        bool blocked = (block.collider != null);
        if (blocked == true)
        {
            return false;
        }

        if (isAlive == false)
        {
            return false;
        }

        return true;
    }


    public Vector2 GetPlayerPosition()
    {
        if (player != null)
        { 
        return player.position;
        }

        return transform.position;
    }
   
}
