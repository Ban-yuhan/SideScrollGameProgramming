using UnityEngine;
using UnityEngine.UI;

public class GoalTrigger : MonoBehaviour
{
    /*
    [SerializeField]
    private StageClearUI clearUI;
    */

    [SerializeField]
    private LevelFlowController lfController;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            /*
            LevelTimer levelTimer = FindAnyObjectByType<LevelTimer>();
            if (levelTimer != null)
            { 
                levelTimer.FinishAndSave();
            }

            
            if(clearUI != null)
            {
                clearUI.showGameClearUI();
                Time.timeScale = 0f;
            }
            */

            if (lfController != null)
            {
                lfController.NotifyGoalTouched();
            }
        }
    }
}
