using UnityEngine;

public class WorldSpaceCanvasBinder : MonoBehaviour
{
    public void BindCamera(Camera cam)
    { 
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            if (canvas.renderMode == RenderMode.WorldSpace) //게임 공간에 canvas를 그리도록 모드가 지정되어 있으면
            { 
                canvas.worldCamera = cam;
            }
        }
    }
}
