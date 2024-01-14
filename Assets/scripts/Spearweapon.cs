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
    [SerializeField] Vector2 spearAttackSize = new Vector2(4f, 2f);
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

        timer = timeToAttack;
        if(playerMove.lastHorizontalVector > 0)
        {
            rightSpearObject.SetActive(true); 
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rightSpearObject.transform.position, spearAttackSize, 0f);
            ApplyDamage(colliders);
        }
        else 
        {
            leftSpearObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(leftSpearObject.transform.position, spearAttackSize, 0f);
            ApplyDamage(colliders);
        }
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        for ( int i = 0; i < colliders.Length; i++)
        {
            Debug.Log(colliders[i].gameObject.name);
        }
    }
}
