using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public GameScore gameScore;
    public TMP_Text textScore;


    
    void Update()
    {
        //int score = gameScore.GetScore();
        int score = gameScore.CurrentScore;
        textScore.text = "Score : " + score.ToString(); //ToString - int형인 score를 string형으로 변환
    }
}
