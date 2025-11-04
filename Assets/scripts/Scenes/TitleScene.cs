using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    
    public FadeScreen fadeScreen;
    public string nextScene;

    void Start()
    {
        //페이드 아웃이 종료됐을 때 호출할 이벤트 함수 등록
        fadeScreen.OnfinishedFadeOut += MoveToNextScene;
    }


    private void OnDisable()
    {
        //등록한 이벤트 함수 해제.
        fadeScreen.OnfinishedFadeOut -= MoveToNextScene;
    }


    void MoveToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
