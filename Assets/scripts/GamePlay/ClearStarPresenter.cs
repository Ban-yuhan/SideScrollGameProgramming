using UnityEngine;
using TMPro;


//사망 카운트, 남은 시간을 읽어와 별 개수를 계산하고 UI에 표시하는 스크립트
public class ClearStarPresenter : MonoBehaviour
{

    [Header("참조")]
    [SerializeField] private LevelConstraints constraints;
    [SerializeField] private StarRatingCalculator calculator;
    [SerializeField] private TMP_Text starText;

    [Header("표시 포맷")]
    [SerializeField] private string format = "stars : {0} / 3"; //{0}을 써놓으면 0을 원하는 값으로 변경 가능.

    private void OnEnable()
    {
        if (constraints == null)
        {
            constraints = FindAnyObjectByType<LevelConstraints>();
        }

        if (calculator == null)
        {
            calculator = GetComponent<StarRatingCalculator>();
            if (calculator == null)
            { 
            calculator = gameObject.AddComponent<StarRatingCalculator>();
            }
        }

        if (constraints == null)
        {
            Debug.LogWarning("ClearStarPresenter: LevelConstraints를 찾지 못했습니다.");
            return;
        }

        float remaining = constraints.GetRemainingTime();
        int deaths = constraints.GetDeaths();

        int stars = 1;

        if (calculator != null)
        { 
        stars = calculator.CalculateStars(remaining, deaths);
        }

        if (starText != null)
        {
            starText.text = string.Format(format, stars); //format의 {0}을 stars 값으로 변경
            // "Stars: {0}/3", stars → "stars : 3/3"
        }
        else 
        {
            Debug.Log("ClearStarpresenter: 별 점수 = " + stars.ToString());
        }
    }
}
