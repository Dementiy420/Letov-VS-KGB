using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Letov : MonoBehaviour
{
    private Rigidbody2D rb; // ������ ������ ��� ��������

    public Vector2 vct; // ������� ������� � 2D ������������
    
    public SpriteRenderer spr; // ������ ������� ��� 2d �������

    public LayerMask Ground; // ���������� ����, �� �������� ����� ��������

    public Animator anim; // ���������� ���������� �� ������������ ��������

    public Image[] Health; // ������ ����������� ��� ����������� ��������

    public GameObject loseGame;

    public Transform GroundCheck; // ���������� �������, ������� �������� �� �������� ���� Ground

    public TMP_Text VinilCount;

    public Sprite fullhp; //����������� ������ ������� ��������
    public Sprite lowhp; //����������� ������ ������� ��������


    [SerializeField] public GameObject Nota; // ������� ������ � ���� ����, ������� ������� ���� �����

    [SerializeField] public Transform ShotPoint; // ������ ������, �� �������� ����� �������� ������ Nota
    private float timeShot; // ����� �������� ��� ��� ����
    private bool WayLeft = false; // ����������, �������� �� �������� ����� ��� ���
    public static int countVin = 0;

    public bool Onground; // ����������, ����� �� �������� �� �����
    public float checkRadius = 0.3f; // ������ ����������� �����


    [SerializeField] public float speed = 4.5f; // �������� ���������

    [SerializeField] public float jump = 300f; // ���� ������ ���������

    [SerializeField] public int heal = 3; // ���������� �������� ���������

    [SerializeField] public int numHeal; //���������� ���������� ������ � ������ �����

    [SerializeField] public float startshot = 0.4f; // ����� ������� ���������� �������

    private void Start() // ����� ���������� 1 ��� � ��������� ��� ��������� �� ���������� ��������
    {
        rb = GetComponent<Rigidbody2D>(); //���������� ������
        anim = GetComponent<Animator>(); //���������� ��������
        spr = GetComponent<SpriteRenderer>(); // ���������� ���������� ��������
    }

    private void Update() // �����, ����������� �������� ��� ���������� �����
    {
        Walk(); //������������ ������
        Flip(); //������� ������
        Jump(); //������
        ChekingGround(); //�������� �����
        HP(); //��������
        Shoot(); //��������
    }
    public void Damage(int dam)
    {
        heal -= dam;
    }

    private void Walk() //�����, ���������� ������� ������������
    {
        vct.x = Input.GetAxis("Horizontal"); //������������ �� ��� x ����� ������� "A", "D" � �������
        rb.velocity = new Vector2(vct.x * speed, rb.velocity.y); // ������������ ��������� �� �����
        anim.SetFloat("movex", Mathf.Abs(vct.x)); // ������������ �������� ��� ������������
    }

    private void Jump() //�����, ���������� ������� ������
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            Physics2D.IgnoreLayerCollision(7, 9, true);
            Invoke("IgnoreOff", 0.5f);
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && Onground) // ���� ������ ������ "W" ��� ������, �� ����������� ��� ������
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jump);
        }
    }

    private void IgnoreOff() 
    {
        Physics2D.IgnoreLayerCollision(7, 9, false);
    }

    private void ChekingGround() // �������� ����� ��� ���������� � ������������ ��������, ����� �������� ������������
    {
        Onground = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground); //��������, �������� �� ������ � ������ �������
        anim.SetBool("onGround", Onground); // �������� ������
    }

    private void Flip() // ������� ��������� 
    {
        if (vct.x < 0f && WayLeft == false)
        {
            transform.Rotate(0f, 180f, 0f);
            WayLeft = true;
        }
        else if (vct.x > 0f && WayLeft == true)
        {
            transform.Rotate(0f, 180f, 0f);
            WayLeft = false;
        }
    }

    private void HP()
    {
        if (heal > numHeal)
        {
            heal = numHeal;
        }
        for (int i = 0; i < Health.Length; i++)
        {
            if (i < Mathf.RoundToInt(heal))
            {
                Health[i].sprite = fullhp;
            }
            else
            {
                Health[i].sprite = lowhp;
            }

            if (i < numHeal)
            {
                Health[i].enabled = true;
            }
            else
            {
                Health[i].enabled = false;
            }
        }
        if (heal <= 0) 
        {
            loseGame.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void Shoot() // �������� ������
    {
        if (timeShot <= 0) //��� ������� ��� timeShot <= 0, ����� ������������� �������. 
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.SetTrigger("Attack");
                Instantiate(Nota, ShotPoint.position, transform.rotation); // ��������� ������� �� �����
                timeShot = startshot; //�������������� ��������, ����� ������� ����� ��� ���� �������
            }
        }
        else // �����, ��� timeShot > 0 ������ ����������� �� ������ �������� 
            timeShot -= Time.deltaTime; //Time.deltaTime �������� �� ���������� ����� �� ��������, � ������� ���� ���������� �����
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Vinil")
        {
            countVin++;
            if (countVin > 3) 
            {
                countVin = 3;
            }
            VinilCount.text = countVin.ToString();
            Destroy(coll.gameObject);
        }
    }
}