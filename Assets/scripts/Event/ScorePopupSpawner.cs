using UnityEngine;

//플레이어가 코인을 획득할 시 화면에 UI를 호출하는 클래스
public class ScorePopupSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject popupPrefab;

    [SerializeField]
    private Camera cameraForWorldCanvas; //UI를 UI영역이 아닌 world공간(게임 플레이화면)에 출력하기 위해 카메라를 참조할 변수

    private void OnEnable()
    {
        GameplayEvents.OnCoinCollected += HandleCoinCollected; //함수의 활성화(+=)
    }

    private void OnDisable()
    {
        GameplayEvents.OnCoinCollected -= HandleCoinCollected; //함수의 비활성화(-=)
    }

    void HandleCoinCollected(Vector3 worldPos, int value) //등록용 변수(OnCoinCollected)의 파라미터의 자료형과 함수의 파라미터의 자료형이 동일해야만 등록할 수 있음
    {
        if (popupPrefab != null)
        {
            GameObject go = Instantiate(popupPrefab, worldPos, Quaternion.identity);
            if (go != null)
            { 
                FloatingText ft = go.GetComponent<FloatingText>();
                if(ft != null)
                {
                    ft.SetText("+" +  value.ToString());
                }
            }

            WorldSpaceCanvasBinder binder = go.GetComponent<WorldSpaceCanvasBinder>();
            {
                if (binder != null)
                {
                    binder.BindCamera(cameraForWorldCanvas);
                }
            }
        }
    }

}
