using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{

    public float respawnDelay = 0.0f;
    public Rigidbody2D rb;
    
    private Vector3 currentSpawnPosition;
    private Vector3 OriginSpawn;
    private bool hasSpawnPosition = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpawnPosition = transform.position;
        OriginSpawn = transform.position;
        hasSpawnPosition = false ;
    }


    public void SetSpawnPosition(Vector3 worldPositioni)
    { 
        currentSpawnPosition = worldPositioni;
        hasSpawnPosition = true ;
        Debug.Log("위치가 저장 되었습니다");
        Debug.Log("position x = " + currentSpawnPosition.x + ", y = " + currentSpawnPosition.y + ", z = " + currentSpawnPosition.z);
    }


    
    public void Respawn()
    {
        if (hasSpawnPosition == true)
        {
            Vector2 v = rb.linearVelocity;
            v.x = 0f;
            v.y = 0f;

            rb.linearVelocity = v;

            /*
            rb.linearVelocity.x = 0f;
            rb.linearVelocity.y = 0f;
            로 하지 않는 이유 → linearVelocity 의 속성에 직접 접근해 바꿀 수 없도록 만들어져 있음.

            대신 간략화를 위해 아래처럼 할 수 있음

            rigidBody.linearVelocity = Vector2.zero; → zero : 제로백터. x,y값에 모두 0을 반환

            */

            transform.position = currentSpawnPosition;
        }
        else
        { 
        transform.position = OriginSpawn;
        }
    }

}
