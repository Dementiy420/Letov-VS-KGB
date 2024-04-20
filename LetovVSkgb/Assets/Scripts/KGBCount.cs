using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KGBCount : MonoBehaviour
{
    TMP_Text text;
    public int con;
    public static int count;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = count.ToString();
        con = int.Parse(text.text);
        Debug.Log(con);
    }
}
