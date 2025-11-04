using System;
using UnityEngine;

public static class GameplayEvents 
{

    //static 클래스의 멤버변수 및 함수는  static으로 선언되어야 함
    public static event Action<Vector3, int> OnCoinCollected;
    //함수를 담을 수 있는 변수 - OnCoinCollected 변수에 값이 아닌 함수(Vector3, int형식의)를 넣을 수 있음.
    public static event Action<Vector3> OnPlayerHit;


    public static event Action<int> OnChangedPlayerHP;


    //==================================================================================
    public static event Action<Vector3> OnEnemyDefeated;
    public static event Action<float> OnHardLanding;
    //==================================================================================
    public static void RaiseCoinCollected(Vector3 worldPosition, int value)
    {
        if (OnCoinCollected != null)
        {
            OnCoinCollected.Invoke(worldPosition, value);
        }
    }

    public static void RaisePlayerHit(Vector3 worldPosition)
    {
        Action<Vector3> handler = OnPlayerHit;
        if (handler != null)
        {

            handler.Invoke(worldPosition);
        }
    }

    public static void RaisePlayerChangedHP(int currentHP)
    {

        Action<int> handler = OnChangedPlayerHP;
        if(handler != null)
        {
            handler.Invoke(currentHP);
        }

    }

    public static void RaiseEnemyDefeated(Vector3 hitPos)
    {
        Action<Vector3> handler = OnEnemyDefeated;
        if (handler != null)
        { 
            handler.Invoke(hitPos);
        }
    }

    public static void RaiseHardLanding(float impact)
    { 
    Action<float> handler = OnHardLanding;
        if (handler != null)
        { 
            handler.Invoke(impact);
        }
    }

    

}
