using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public PlayerRespawn respawn;
    public int maxHP = 3;
    private int currentHP;


    private void Start()
    {
        InitHP();
    }

    void InitHP()
    {
        currentHP = maxHP;
        GameplayEvents.RaisePlayerChangedHP(currentHP);
    }




    public void ApplyDamage()
    {

        --currentHP;
        GameplayEvents.RaisePlayerChangedHP(currentHP);

        Debug.Log("hp : " + currentHP);

        if (currentHP == 0)
        {
            StartCoroutine(Respawn());   
        }
    }


    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);

        if (respawn != null)
        {
            respawn.Respawn();
            InitHP();
        }
    }

    public bool isAlive()
    {
        if( currentHP == 0)
        {
            return false;
        }
        return true;
    }
  
}
