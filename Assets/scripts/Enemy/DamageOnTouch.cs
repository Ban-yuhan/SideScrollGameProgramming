using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isPlayer = collision.collider.CompareTag("Player") == true; // 충돌한 오브젝트의 태그가 player면 true가, 그렇지 않다면 false가 넘어감
        if (isPlayer == true )
        {
            /*
            PlayerRespawn pr = collision.collider.GetComponent<PlayerRespawn>();
            if (pr != null)
            {
                pr.Respawn();
            }
            
            //적과 충돌 시 바로 리스폰 하지 않고 체력 감소를 적용시키기 위해 주석 처리
            */
            
            PlayerHealth ph = collision.collider.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.ApplyDamage();
            }
        }
    }
}
