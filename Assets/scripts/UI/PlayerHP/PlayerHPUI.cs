using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{

    public Image[] HpImage;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    
    public void UpdateHPUI(int currentHP)
    {
        if (HpImage != null && HpImage.Length != 0f)
        {
            for (int i = 0; i < HpImage.Length; ++i)
            {
                HpImage[i].gameObject.SetActive(false);
            }

            for(int i =0; i<currentHP; ++i)
            {
                HpImage[i].gameObject.SetActive(true);
            }
        }
    }


    private void OnEnable()
    {
        GameplayEvents.OnChangedPlayerHP += UpdateHPUI;

    }

    private void OnDisable()
    {
        GameplayEvents.OnChangedPlayerHP -= UpdateHPUI;
    }
}
