using UnityEngine;

public class SfxOneShotPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinClip; //AudioCoip : 실제 사용 할 사운드 파일을 연결

    [SerializeField]
    private float volume = 0.9f;


    private void OnEnable()
    {
        GameplayEvents.OnCoinCollected += HandleCoin;
    }


    private void OnDisable()
    {
        GameplayEvents.OnCoinCollected -= HandleCoin;
    }


    private void HandleCoin(Vector3 pos, int value)
    {
        if (coinClip != null)
        {
            AudioSource.PlayClipAtPoint(coinClip, pos, volume); //실행할 사운드파일과 위치, 볼륨을 설정
        }
    }

}
