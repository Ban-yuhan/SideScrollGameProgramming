using UnityEngine;

public class GameScore : MonoBehaviour
{
    public int startScore = 0;

    private int currentScore = 0;

   
    public int CurrentScore //프로퍼티 사용
    {
        get
        {
            return currentScore;
        }
    }
   

    private void Awake()
    {
        ResetScore();
    }


    public void AddScore(int amount)
    { 
        currentScore += amount; // currentScore = currentScore + amount;와 동일
    }

    /*
    public int GetScore()
    {
        return currentScore;
    }
    */

    public void ResetScore()
    {
        currentScore = startScore;
    }
}
