using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "MainHero")
        {
            SceneManager.LoadScene("Menu");
            Letov.countVin = 0;
        }
    }
}