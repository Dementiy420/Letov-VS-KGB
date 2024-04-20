using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nota : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float distance;
    [SerializeField] public int damage;
    public LayerMask WhatIsSolid;
    private void Start()
    {
        StartCoroutine(WaitAndDestory(3.0f));
    }
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, WhatIsSolid);
        if (hitInfo.collider != null) 
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                KGBCount.count += 20;
                hitInfo.collider.GetComponent<KGB>().TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    private IEnumerator WaitAndDestory(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }
}