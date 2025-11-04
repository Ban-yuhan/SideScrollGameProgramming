using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


//현재 스테이지의 진행 상황을 관리
public class LevelFlowController : MonoBehaviour
{
    [Header("목표 설정")]
    [SerializeField] private int requiredScore = 10;

    [Header("참조")]
    [SerializeField] private GameScore gameScore;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private SceneTransitionController transition;


    public event Action OnStageCleared; //event형식으로 함수 호출


    private bool readyToClear = false;
    private int currentScore = 0;
    private bool cleared = false;



    private void Awake()
    {
        if (clearPanel != null)
        {
            clearPanel.SetActive(false);
            PausePanel.SetActive(false);
        }

        if (gameScore != null)
        {
            currentScore = gameScore.GetScore();
        }
        else
        {
            Debug.LogWarning("LevelFlowController: gameScore가 설정되지 않았습니다.");
        }

        UpdateReadyState();
        UpdateObjectiveUI();
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Pause();
        }
        */
    }

    private void OnEnable()
    {
        if (gameScore != null)
        {
            gameScore.OnScoreChanged += HandleScoreChanged; //이벤트 함수 활성화
        }
    }

    private void OnDisable()
    {
        if (gameScore != null)
        {
            gameScore.OnScoreChanged -= HandleScoreChanged; //이벤트 함수 해제
        }
    }

    public void OnclickedPause()
    {
        Pause();

    }

    public void OnclikedResume()
    {

        Resume();
    }

    public void OnclickMenu()
    {
        transition.LoadSceneByName("SelectScene");
        PausePanel.SetActive(false );
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Pause()
    {
        Debug.Log("버튼눌림");
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
   


private void HandleScoreChanged(int newScore) //갱신된 점수값을 받아 currentScore에 저장. 현재State와 UI에 갱신
    {
        currentScore = newScore;
        UpdateReadyState();
        UpdateObjectiveUI();
    }

    private void UpdateReadyState()
    {
        if (currentScore >= requiredScore) 
        {
            readyToClear = true;
        }
        else
        {
            readyToClear = false;
        }
    }

    private void UpdateObjectiveUI()
    {
        if (objectiveText != null)
        {
            objectiveText.text = "Goal: " + requiredScore.ToString() + "  |  Now: " + currentScore.ToString(); //목표 스코어 및 현재 스코어를 text로 표시
        }
    }

    public void NotifyGoalTouched()
        // 클리어 포탈에 플레이어가 닿았을 때 호출.
    {
        Debug.Log("readyToClear : " + readyToClear);

        if (readyToClear == true)
        {
            DoClear();
        }
        else
        {
            Debug.Log("Goal 도달. 아직 목표 점수가 부족합니다. (" + currentScore.ToString() + " / " + requiredScore.ToString() + ")");
        }
    }

    private void DoClear()
    {
        if (cleared == true)
        {
            return;
        }

        cleared = true;

        if (clearPanel != null)
        {
            clearPanel.SetActive(true); //클리어 UI 활성화
        }

        // 1) 저장·보고가 먼저 수행될 수 있도록 이벤트를 먼저 발행.
        Action handler = OnStageCleared;
        if (handler != null)
        {
            handler.Invoke();
        }

        // 2) 게임 정지.
        Time.timeScale = 0.0f;
        Debug.Log("Stage Clear!");
    }

    

    

    // 공개 Getter (연동용)
    public bool IsReadyToClear()
    {
        return readyToClear;
    }

    public int GetRequiredScore()
    {
        return requiredScore;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
