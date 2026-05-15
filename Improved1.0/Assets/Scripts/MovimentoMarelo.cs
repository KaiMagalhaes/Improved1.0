using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentoMarelo : MonoBehaviour
{
    public float vel = 6f;
    public float pulo = 12f;
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
        float h = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(h * vel, rb.linearVelocity.y);

        if (h != 0) transform.localScale = new Vector3(h > 0 ? 1 : -1, 1, 1);

        anim.SetBool("andar", h != 0 && noChao);
        anim.SetBool("noAr", !noChao);

        if (Input.GetButtonDown("Jump") && noChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, pulo);
            noChao = false;
        }

        if (transform.position.y < -20f) Reiniciar();
    }

    void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Portal"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnCollisionEnter2D(Collision2D outro)
    {
        if (outro.gameObject.CompareTag("Chao")) noChao = true;
    }

    void OnCollisionExit2D(Collision2D outro)
    {
        if (outro.gameObject.CompareTag("Chao")) noChao = false;
    }
}