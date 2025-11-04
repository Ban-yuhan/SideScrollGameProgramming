using UnityEngine;

//Asset Menu에 나만의 메뉴를 생성하겠다 - 생성되는 파일이름은 LevelMeta, Asset메뉴에 생성할 메뉴 이름은 Game항목의 LevelMeta로 하겠다.
[CreateAssetMenu(fileName = "LevelMeta", menuName = "Game/Level Meta", order = 0)]


public class LevelMeta : ScriptableObject
{

    public string displayName;
    public string sceneName;
    public string levelId;
    public Sprite preview;
    public bool initiallyLocked = false;

}
