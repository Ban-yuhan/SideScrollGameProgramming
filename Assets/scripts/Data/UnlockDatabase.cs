using UnityEngine;

//파일 : UnlockDatabase.cs
//역할 : 여러 LevelUnlockRule을 묶어 관리하고, levelId로 규칙을 찾아준다
[CreateAssetMenu(fileName = "UnlockDatabase", menuName = "Game/Unlock Database", order = 1)]
public class UnlockDatabase : ScriptableObject
{
    [Header("레벨 해금 규칙 목록")]
    public LevelUnlockRule[] rules;

    public LevelUnlockRule FindRule(string levelID)
    {
        if (rules == null)
        { 
            return null;
        }

        for (int i = 0; i < rules.Length; i = i + 1)
        {
            LevelUnlockRule r = rules[i];

            if (r == null)
            {
                continue;
            }
            if (string.Equals(r.levelID, levelID) == true) // if(r.levelID == levelID) 라고 쓴 것과 같음. 다만 메모리를 더 적게먹음 / string.Equals(a,b) → 문자열 a과 b가 같으면 true. 다르면 false 반환
            {
                return r;
            }
        }
        return null;
    }
}
