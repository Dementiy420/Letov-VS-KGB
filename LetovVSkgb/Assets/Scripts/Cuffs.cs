using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuffs : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] public float speed;
    [SerializeField] public float distance;
    [SerializeField] public int uron = 1;
    public LayerMask WhatIsLetov;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WaitAndDestory(3.0f));
        rb.velocity = transform.right * speed;
    }
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, WhatIsLetov);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("MainHero"))
                hitInfo.collider.GetComponent<Letov>().Damage(uron);

            Destroy(gameObject);
        }
    }
    private IEnumerator WaitAndDestory(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}