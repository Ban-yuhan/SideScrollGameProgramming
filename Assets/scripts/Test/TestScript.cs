using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform TestObject;

    void Start()
    {
        Debug.Log("Hello, World");


        int a = 12;
        int b = a;

        int A;
        float B;

        B = 1.34f;
        A = (int)B;
    }

}
