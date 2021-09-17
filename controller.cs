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
        if (PlayerPrefs.HasKey("ABC"))
        {
            coins = PlayerPrefs.GetInt("ABC");
            TextCoins.text = coins.ToString();
        }
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        move = transform.right * horizontal + transform.forward * vertical;
        
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded == true) 
        {
            characterController.Move(move * run * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * speed * Time.deltaTime); 
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Mon")
        {
            coins++;
            other.gameObject.SetActive(false); 
            TextCoins.text = coins.ToString();
            PlayerPrefs.SetInt("ABC", coins);
        }
    }
}
