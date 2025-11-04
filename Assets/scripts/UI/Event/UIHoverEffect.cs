using UnityEngine;
using UnityEngine.EventSystems;


// 스테이지 선택에서 마우스를 올리면 확대시키는 기능 수행
public class UIHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private float normalScale = 1f; //카드 목록이 평상시 상태일때의 크기

    [SerializeField]
    private float hoverScale = 1.3f;

    [SerializeField]
    private float scaleLerpPerSecond = 12.0f; //크기 변화 속도

    [SerializeField]
    private bool useSmoothStep = true; //부드럽게 전환 할 것인가

    private bool hover = false;
    private Vector3 targetScale;

    void Awake()
    {
        targetScale = new Vector3(normalScale, hoverScale, 1.0f);
        transform.localScale = targetScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
    hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }

    private void Update()
    {
        float desired = normalScale;

        if (hover == true)
        { 
            desired = hoverScale;
        }

        Vector3 current = transform.localScale; //현재 크기
        Vector3 desiredV = new Vector3(desired, desired, 1.0f); //변경 할 크기

        float t = scaleLerpPerSecond * Time.unscaledDeltaTime; //0~1값이어야함

        if (t > 1.0f) //1을 넘어서면 1로 고정
        {
            t = 1.0f;
        }

        if(useSmoothStep == true)
        {
            float s = Mathf.SmoothStep(0.0f, 1.0f, t);
            Vector3 next = new Vector3(Mathf.Lerp(current.x, desiredV.x, s), Mathf.Lerp(current.y, desiredV.y, s), 1.0f); //x, y값의 scale을 부드럽게 변경

            transform.localScale = next;
        }
        else
        {
            Vector3 next = new Vector3(Mathf.Lerp(current.x, desiredV.x, t), Mathf.Lerp(current.y, desiredV.y, t), 1.0f); 

            transform.localScale = next;
        }
    }

}
