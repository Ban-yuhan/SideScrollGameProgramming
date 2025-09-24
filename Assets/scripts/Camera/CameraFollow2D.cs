using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5.0f;

    private void LateUpdate()
    {
        Vector3 current = transform.position; //현재 카메라의 위치
        Vector3 desired = new Vector3(target.position.x, target.position.y, target.position.z); //타겟의 위치(이동해야 할 위치)
        Vector3 smooth = Vector3.Lerp(current, desired, followSpeed * Time.deltaTime);  //시작점에서 목표지점까지 지정된 속도로 부드럽게 이동 및 회전을 하고싶을 때 사용
        transform.position = new Vector3(smooth.x, smooth.y, current.z); //카메라의 좌표 수정
    }
}
