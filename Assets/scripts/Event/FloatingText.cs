using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{

    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private float lifeTime = 0.8f; //텍스트 유지 시간

    [SerializeField]
    private float riseSpeed = 1.0f; //텍스트가 올라가는 속도

    [SerializeField]
    private float fadeOutStart = 0.3f; //텍스트가 사라지기 시작하는 시간

    private float alive = 0f;
    private Color initialColor;




    private void Awake()
    {
        if (text != null)
        {
            initialColor = text.color;
        }
    }




    public void SetText(string content)
    {
        if (text != null)
        {
            text.text = content;
        }
    }




    void Update()
    {
        alive += Time.deltaTime;

        Vector3 p = transform.position;
        p.y = p.y + riseSpeed * Time.deltaTime; //텍스트의 y위치를 변경
        transform.position = p; //변경된 y값을 적용

        if (text != null)
        {
            float t = Mathf.Clamp01((alive - fadeOutStart) / Mathf.Max(0.0001f, lifeTime -fadeOutStart));
            //Mathf.Clamp01 : ()안의 값의 0과 1 사이를 벗어나지 않도록 해줌.
            //Mathf.Max : ()안의 값 중 더 큰값을 반환

            float alpha = 1.0f - t; //투명도를 조금씩 감소시켜줌
            Color c = initialColor;
            c.a = alpha; //감소된 투명도를 적용
            text.color = c;
        }

        if (alive >= lifeTime)
        { 
            Destroy(gameObject);
        }
    }
}
