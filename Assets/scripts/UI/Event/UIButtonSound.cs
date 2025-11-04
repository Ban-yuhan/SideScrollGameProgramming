using UnityEngine;
using UnityEngine.EventSystems;

//마우스가 올라갔을 때, 클릭했을 때 사운드 재생
public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    [SerializeField]
    private AudioClip hoverClip; //마우스를 올렸을 때 사용할 오디오클립

    [SerializeField]
    private AudioClip clickClip; //클릭했을 때 사용할 오디오클립

    [SerializeField]
    private float volume = 0.8f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverClip != null)
        {
            AudioSource.PlayClipAtPoint(hoverClip, Vector3.zero, Mathf.Clamp01(volume)); // volume을 0~1 사이로 제한
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickClip != null)
        {
            AudioSource.PlayClipAtPoint(clickClip, Vector3.zero, Mathf.Clamp01(volume)); // volume을 0~1 사이로 제한
        }
    }

}
