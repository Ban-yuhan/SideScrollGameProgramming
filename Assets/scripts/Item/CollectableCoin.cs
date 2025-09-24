using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    public int value = 1; //코인 획득 시 얻는 점수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")== true) //충돌시 정보가 collision에 들어감. Player 태그를 단 오브젝트와 충돌했을 시 아래 이행
        { 

            GameScore gameScore = GameScore.FindAnyObjectByType<GameScore>(); //FindAnyObjectByType - <>안에 해당하는 컴포넌트를 찾아 반환
            if (gameScore != null)
            { 
                gameScore.AddScore(value);
            }
            Destroy(gameObject);
        }
    }

}
