using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingPlayer : MonoBehaviour 
{
    public OldMan_Npc oldman;
    private Man_Npc man;
    private Female_Npc female;

    public Transform player;

    private void Awake()
    {
        //oldman = GetComponent<OldMan_Npc>();
        man = GetComponent<Man_Npc>();
        female = GetComponent<Female_Npc>();
    }

    public void LookPlayerOldMan()
    {
        //if(Vector3.Distance(player.transform.position, oldman.transform.position) > 5f)
        //{
            oldman.transform.LookAt(player);
        //}
        if (Vector3.Distance(player.transform.position, oldman.transform.position) < 5f)
        {
            man.transform.LookAt(player);
        }
        if (Vector3.Distance(player.transform.position, oldman.transform.position) < 5f)
        {
            female.transform.LookAt(player);
        }
    }
}
