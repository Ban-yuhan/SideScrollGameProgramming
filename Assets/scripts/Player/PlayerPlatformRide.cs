using UnityEditor.Compilation;
using UnityEngine;

public class PlayerPlatformRide : MonoBehaviour
{
    public Rigidbody2D rb;
    private MovingPlatform movingPlatform;
    public float verticalRiseThreshold = 0.01f;

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        movingPlatform = collision.collider.GetComponent<MovingPlatform>(); //충돌한 오브젝트의 컴포넌트 중 <> 에 해당하는 컴포넌트를 가져온다.
        if (movingPlatform != null)
        {
            transform.SetParent(movingPlatform.transform); //부모로 할 오브젝트의 transform을 입력.
        }
    }

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        MovingPlatform platform = collision.collider.GetComponent<MovingPlatform>();
        if ((platform != null))
        {
            
                if (platform == movingPlatform)
                {
                    movingPlatform = null;
                    transform.SetParent(null);
                }
            
        }
    }
    
    

    private void FixedUpdate()
    {
        if (movingPlatform != null)
        {
            if (rb.linearVelocity.y > verticalRiseThreshold) //점프키를 눌렀을 때 발판에서의 처리를 무시하도록 함
            {
                return;
            }

            Vector2 v = rb.linearVelocity;
            v = v + movingPlatform.CurrentVelocity;
            rb.linearVelocity = v;
        }
    }
  
    
}
    
    