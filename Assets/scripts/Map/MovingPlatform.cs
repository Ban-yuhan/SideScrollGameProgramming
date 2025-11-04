using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatform : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform[] wayPoints;
    public float moveSpeed = 2.0f;
    public float arriveThreshold = 0.05f; //waypoint와 platform이 완벽히 일치했을때만 인식하게되면 렉이 걸렸을때 큰 폭으로 platform이 이동하게되면 waypoint를 지나칠 수 있음. → 해당 범위에 근접하면 인식해 닿은것으로 처리

    private int currentIndex = 0; //platform이 목표로 지정하고있는 waypoint가 몇 번째 waypoint인지 순서를 저장.

    public Vector2 CurrentDelta { get; private set; } //쓰기는 금지하고, 읽기만 가능한 프로퍼티.

    public Vector2 CurrentVelocity { get; private set; }

    
    

    private void Start()
    {
        transform.position = wayPoints[0].position;
        
}

    private void FixedUpdate()
    {
        CurrentDelta = Vector2.zero;
        CurrentVelocity = Vector2.zero;
        Vector2 before = rb.position;

        Transform target = wayPoints[currentIndex];
        Vector2 current = rb.position;
        Vector2 targetPos = target.position;
        Vector2 toTarget = targetPos - current;


        //float distance = toTarget.magnitude; //벡터의 크기(길이)를 구해줌. 아래와 같이 해도 같은 결과를 얻을 수 있음
        float distance = Vector2.Distance(targetPos, current); // Unity에서 제공해주는 두 지점 사이의 거리를 구해주는 기능. 

        if (distance <= arriveThreshold)
        {
            ++currentIndex; // ++ → 변수 안의 데이터를 1 씩 증가. 다음과 같음 currentIndex = currentIndex + 1; 또는 currentIndex += 1
            
            if (currentIndex >= wayPoints.Length)
            {
                currentIndex = 0;
            }
        }

        Vector2 dir = toTarget.normalized;
        Vector2 next = current + dir * moveSpeed * Time.fixedDeltaTime; //이전 fixedUpdate 에서 현제 fixdUpdate까지 오는데 걸린 시간.
        rb.MovePosition(next);


        CurrentDelta = next - before; // 다음 위치에 이전 위치를 빼서 얼만큼 이동했는지를 저장함
        if (Time.fixedDeltaTime > 0.0f)
        { 
        CurrentVelocity = CurrentDelta / Time.fixedDeltaTime;
        }
    }


}
