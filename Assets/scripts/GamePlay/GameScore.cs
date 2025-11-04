using UnityEngine;
using System.Collections;
using System;

public class GameScore : MonoBehaviour
{
    public int startScore = 0;

    private int currentScore = 0;
    [SerializeField] private AudioClip preloadedNextClip;
    [SerializeField] private float Volume;
    [SerializeField] private bool playImmediateOnStart = false;

    public BgmController ctrl;
    private bool hascleared = false;    

    public int CurrentScore //프로퍼티 사용
    {
        get
        {
            return currentScore;
        }
    }


    public int maxScore = 10;
    public GameObject goalPortal;

    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        ResetScore();

        if(goalPortal != null) //유효성 검사. 데이터가 유효한 데이터인지 검사. null 체크라고도 부름.
        {
           goalPortal.SetActive(false);

        }
    }


    public void AddScore(int amount)
    { 
        currentScore += amount; // currentScore = currentScore + amount;와 동일
        if (OnScoreChanged != null)
        {
            OnScoreChanged.Invoke(currentScore);
        }

        if(currentScore >= maxScore && !hascleared)
        {
            if (goalPortal != null)
            {
                goalPortal.SetActive(true);
                StartCoroutine(CanClear());

              
                hascleared = true;
            }
        }
    }

    
    private IEnumerator CanClear()
    {
        if (ctrl != null)
        {
            ctrl.CrossfadeTo(preloadedNextClip, Volume, 1.2f);
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
  

    
    public int GetScore()
    {
        return currentScore;
    }
    

    public void ResetScore()
    {
        currentScore = startScore;
    }
}
