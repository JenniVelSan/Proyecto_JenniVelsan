using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float fuerzaSalto;
    public GameManager gameManager;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()

    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.start)
        if (Input.GetKeyDown (KeyCode.Space))
        {
                animator.SetTrigger("Saltar"); 
            rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
        }
        if (gameManager.gameOver)
        {
            Destroy(gameObject);
        
    }
}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            animator.SetBool("estaSaltando", false);
        }

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            rigidbody2D.linearVelocity = Vector2.zero; // se frena en seco
            gameManager.gameOver = true;
           
        }
    }
}

