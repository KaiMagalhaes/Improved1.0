using UnityEngine;

public class MovimentoMarelo : MonoBehaviour
{
    public float vel = 6f;
    public float pulo = 12f;
    private Rigidbody2D rb;
    private bool noChao;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * vel, rb.linearVelocity.y);

        if (horizontal > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal < 0) transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetButtonDown("Jump") && noChao)
        {
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
}