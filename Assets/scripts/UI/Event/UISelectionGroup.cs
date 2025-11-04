using UnityEngine;
using System.Collections.Generic; //List를 사용하기 위해 지정한 네임스페이스

public class UISelectionGroup : MonoBehaviour
{

    [SerializeField]
    private UISelectableCard initialSelected; //스테이지 선택 씬이 시작되었을 떄 자동으로 선택상태로 세팅 할 카드 지정 (첫 번째 카드가 자동 지정되도록 할 때 사용)

    private List<UISelectableCard> cards = new List<UISelectableCard>();
    private UISelectableCard current; //현재 선택된 카드 저장

  
    public void Init()
    {
        cards.Clear(); // 카드 리스트 초기화

        UISelectableCard[] found = GetComponentsInChildren<UISelectableCard>(true); //자식 오브젝트에 부착되어있는 컴포넌트를 모두 가져옴 → 자식이 여러개라면 모든 자식의 해당하는 컴포넌트를 가져옴

        for (int i = 0; i < found.Length; ++i)
        {
            cards.Add(found[i]);
            found[i].selectionGroup = this;
        }

        if (initialSelected != null)
        {
            NotifySelected(initialSelected);
        }
    }

    public void NotifySelected(UISelectableCard card)
    {
        if (card == null)
        {
            return;
        }

        if (current == card)
        {
            return;
        }

        if (current != null) 
        {
            current.Deselect(); //현재 카드 정보가 선택된 카드와 다르다면 카드정보를 선택되지않음 으로 전환
        }

        current = card; //현재 카드를 선택된 카드로 전환
        current.Select();
    }
}
