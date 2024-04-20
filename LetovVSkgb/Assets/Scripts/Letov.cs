using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Letov : MonoBehaviour
{
    private Rigidbody2D rb; // задает физику для спрайтов

    public Vector2 vct; // позиция объекта в 2D пространстве
    
    public SpriteRenderer spr; // рендер страйта для 2d графики

    public LayerMask Ground; // объявления слоя, от которого можно прыгнуть

    public Animator anim; // переменная отвечающая за проигрывание анимаций

    public Image[] Health; // массив изображений для отображения здоровья

    public GameObject loseGame;

    public Transform GroundCheck; // объявление объекта, который отвечает за проверку слоя Ground

    public TMP_Text VinilCount;

    public Sprite fullhp; //изображение полной единицы здоровья
    public Sprite lowhp; //изображение пустой единицы здоровья


    [SerializeField] public GameObject Nota; // Игровой объект в виде ноты, который наносит урон врагу

    [SerializeField] public Transform ShotPoint; // пустой объект, из которого будет выходить объект Nota
    private float timeShot; // Время выстрела для для ноты
    private bool WayLeft = false; // определяет, повернут ли персонаж влево или нет
    public static int countVin = 0;

    public bool Onground; // определяет, стоит ли персонаж на земле
    public float checkRadius = 0.3f; // радиус определения земли


    [SerializeField] public float speed = 4.5f; // скорость персонажа

    [SerializeField] public float jump = 300f; // сила прыжка персонажа

    [SerializeField] public int heal = 3; // количество здоровья персонажа

    [SerializeField] public int numHeal; //определяет количество жизней с учетом урона

    [SerializeField] public float startshot = 0.4f; // через сколько начинается выстрел

    private void Start() // метод вызывается 1 раз и выполняет код зависимый от активности объектов
    {
        rb = GetComponent<Rigidbody2D>(); //объявление физики
        anim = GetComponent<Animator>(); //объявление анимаций
        spr = GetComponent<SpriteRenderer>(); // объявление рендеринга спрайтов
    }

    private void Update() // метод, исполняющий действия при обновлении кадра
    {
        Walk(); //передвижение игрока
        Flip(); //поворот игрока
        Jump(); //прыжок
        ChekingGround(); //проверка земли
        HP(); //здоровье
        Shoot(); //стрельба
    }
    public void Damage(int dam)
    {
        heal -= dam;
    }

    private void Walk() //метод, содержащий принцип передвижения
    {
        vct.x = Input.GetAxis("Horizontal"); //передвижение по оси x через клавиши "A", "D" и стрелок
        rb.velocity = new Vector2(vct.x * speed, rb.velocity.y); // передвижение персонажа по сцене
        anim.SetFloat("movex", Mathf.Abs(vct.x)); // проигрывание анимации при передвижении
    }

    private void Jump() //метод, содержащий принцип прыжка
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            Physics2D.IgnoreLayerCollision(7, 9, true);
            Invoke("IgnoreOff", 0.5f);
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && Onground) // если нажата кнопка "W" или пробел, то исполняется сам прыжок
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jump);
        }
    }

    private void IgnoreOff() 
    {
        Physics2D.IgnoreLayerCollision(7, 9, false);
    }

    private void ChekingGround() // проверка земли под персонажем и проигрывание анимации, чтобы избежать мультипрыжка
    {
        Onground = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground); //проверка, попадает ли объект в нужную область
        anim.SetBool("onGround", Onground); // анимация прыжка
    }

    private void Flip() // поворот персонажа 
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

    private void Shoot() // стрельба нотами
    {
        if (timeShot <= 0) //при условии что timeShot <= 0, будет производиться выстрел. 
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.SetTrigger("Attack");
                Instantiate(Nota, ShotPoint.position, transform.rotation); // появление объекта на сцене
                timeShot = startshot; //приравнивается значение, через сколько будет еще один выстрел
            }
        }
        else // иначе, при timeShot > 0 таймер отсчитывает до нового выстрела 
            timeShot -= Time.deltaTime; //Time.deltaTime интервал от последнего кадра до текущего, с помощью него происходит отчет
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