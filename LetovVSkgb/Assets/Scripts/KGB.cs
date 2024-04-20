using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KGB : MonoBehaviour
{
    [SerializeField] public int health = 3;
    [SerializeField] public GameObject Cuffs;
    [SerializeField] public Transform Attack;
    [SerializeField] public float startshot;
    private float timershot;
    public Animator anim;
    public GameObject KBG;


    void Start()
    {
        timershot = startshot;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        shoot();
        if (health <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("Death");
            Destroy(gameObject, 1.3f);
        }
    }
    public void TakeDamage(int damage) 
    {
        health -= damage;
    }
    private void shoot()
    {
        if (health <= 0)
        {
            timershot = 99;
        }
        else
        {
            if (timershot <= 0)
            {
                Instantiate(Cuffs, Attack.position, transform.rotation);
                timershot = startshot;
            }
            else
                timershot -= Time.deltaTime;

        }
    }
}