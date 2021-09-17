using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [Header("Скорость перемещения персонажа")]
    public float speed = 10f;

    [Header("Скорость бега персонажа")]
    public float run = 20f;

    private Rigidbody rb;

    public CharacterController characterController; 

    public int coins;

    public Text TextCoins;

    Vector3 move;

        Vector3 velocity;

    public float gravity = -3200f;

    public Transform groundCheck;

    public float groundDistance = 0.2f;

    public LayerMask groundMask;

    bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (PlayerPrefs.HasKey("ABC"))
        {
            coins = PlayerPrefs.GetInt("ABC");
            TextCoins.text = coins.ToString();
        }
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Переменные для перемещения по горизонтали и вертикали
        float vertical = Input.GetAxis("Vertical");

        move = transform.right * horizontal + transform.forward * vertical;
        
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded == true) // Мы проверяем, что если мы зажимаем левый Shift, и если мы на земле (isGrounded), то скорость перемещение изменится на run
        {
            characterController.Move(move * run * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * speed * Time.deltaTime); // Иначе мы двигаемся с обычной скоростью
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
// !!! 
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("It worked!");
        if (other.gameObject.CompareTag("Mon"))
        {
            coins++; // При столкновении с объектом, общее количество собранных букв мы увеличим на единицу
            other.gameObject.SetActive(false); // После того как столкнулись с объектом, мы его деактивируем
            TextCoins.text = coins.ToString();
            PlayerPrefs.SetInt("ABC", coins);
        }
    }
}
