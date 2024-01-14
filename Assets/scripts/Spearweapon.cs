using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spearweapon : MonoBehaviour
{
    [SerializeField]float timeToAttack = 4f;
    float timer;

    [SerializeField] GameObject leftSpearObject;
    [SerializeField] GameObject rightSpearObject;
    
    playerControle playerMove;
    private void Awake()
    {
        playerMove = GetComponentInParent<playerControle>();
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <0f)
        {
            Attack();
        }
        
    }

    private void Attack()
    {
        Debug.Log("Attack");
        timer = timeToAttack;
        if(playerMove.lastHorizontalVector > 0)
        {
            rightSpearObject.SetActive(true);
            Debug.Log("Attack right");
        }
        else 
        {
            leftSpearObject.SetActive(true);
            Debug.Log("Attack left");
        }
    }
}
