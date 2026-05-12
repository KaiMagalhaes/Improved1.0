using UnityEngine;
using UnityEngine.InputSystem;

public class MareloFix : MonoBehaviour
{
    public float velocidade = 7f;
    public float forcaSalto = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer renderizador;
    private bool noChao = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderizador = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
   
        float move = 0;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            move = -1;
            renderizador.flipX = true;   
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            move = 1;
            renderizador.flipX = false;  
        }

        rb.linearVelocity = new Vector2(move * velocidade, rb.linearVelocity.y);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && noChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaSalto);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Plataforma"))
            noChao = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Plataforma"))
            noChao = false;
    }
}