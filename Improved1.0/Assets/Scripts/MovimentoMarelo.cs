using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MovimentoMarelo : MonoBehaviour
{
    public float vel = 6f;
    public float pulo = 8f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool noChao;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


void Update()
{
    float h = 0;
    if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) h = -1;
    if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) h = 1;

    rb.linearVelocity = new Vector2(h * vel, rb.linearVelocity.y);

    if (h > 0) transform.localScale = new Vector3(1, 1, 1);
    else if (h < 0) transform.localScale = new Vector3(-1, 1, 1);

    anim.SetBool("andar", h != 0);
    anim.SetBool("noAr", !noChao);

    if (Keyboard.current.spaceKey.wasPressedThisFrame && noChao)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * pulo, ForceMode2D.Impulse);
        noChao = false;
    }
}

void OnCollisionEnter2D(Collision2D outro)
    {
        if (outro.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }
    }

    void OnCollisionExit2D(Collision2D outro)
    {
        if (outro.gameObject.CompareTag("Chao"))
        {
            noChao = false;
        }
    }
}