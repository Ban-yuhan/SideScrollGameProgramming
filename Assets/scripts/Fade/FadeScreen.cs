using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image fadeImage;

    public event Action OnfinishedFadeOut;


    public void FadeOut()
    {
        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color; //페이드 이미지의 색상 정보를 가져옴
        color.a = 0.0f; //투명도 0 설정
        fadeImage.color = color;

        StartCoroutine(CoFadeOut());
    }
    

    IEnumerator CoFadeOut()
    {
        // 코루틴 : 1) 함수 내의 코드를 시간 단위로 분할해서 처리할 때 사용하는 함수
        //          2) 비동기 처리를 하기 위해 사용하는 함수    ※ 비동기 처리 : 동작중인 작업이 완료되기 전에 다른 작업을 동시에 진행 하는 것.

        while (true)  //조건 true → 특정한 상황이 아니라면 무한으로 반복
        {
            Color color = fadeImage.color; //페이드 이미지의 색상 정보를 가져옴
            color.a += Time.deltaTime; //매 프레임마다 a값 증가

            if (color.a > 1f)
            {
                color.a = 1f;
            }

            fadeImage.color = color;

            if(fadeImage.color.a == 1f)
            {
                break;
            }

            yield return null;
        }

        if(OnfinishedFadeOut != null)
        {
            OnfinishedFadeOut.Invoke();
        }

        //SceneManager.LoadScene("SelectScene"); //씬 이름을 직접 입력, 또는 build Scene에서 씬 번호를 입력
    }
}
