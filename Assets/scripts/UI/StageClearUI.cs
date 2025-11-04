using UnityEngine;
using TMPro; //TextMeshPro를 사용하기 위한 전처리문
using UnityEngine.SceneManagement; //Scene 관리를 위한 전처리문

public class StageClearUI : MonoBehaviour
{

    [SerializeField]
    private GameObject clearPanel;

    private void Start()
    {
        if (clearPanel != null)
        { 
            clearPanel.SetActive(false);
        }
    }

    public void showGameClearUI()
    { 
        if(clearPanel != null)
        {
            clearPanel.SetActive(true);
        }
    }


    public void OnclicRestartButton()
    {
        //SceneManager.LoadScene("GameScene"); //""내의 이름을 지닌 씬을 로딩
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //현재 열려있는 씬 이름을 참조해 그 씬을 다시 로딩
        Time.timeScale = 1.0f;
    }
}
