
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MovimentoMarelo : MonoBehaviour
{
    public float vel = 6f;
    public float pulo = 6f;
    public TextMeshProUGUI txtPts;
    public TextMeshProUGUI txtOver;
    private Rigidbody2D rb;
    private Animator anim;
    private bool noChao;
    private int pts;
    private float posInicY;
    private GameObject cogSpd;
    private float velOrig;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pts = 0;
        velOrig = vel;
        posInicY = transform.position.y;

        if (txtOver != null) txtOver.gameObject.SetActive(false);

        AtuTxt();
        Sortear();
    }

    void Sortear()
    {
        GameObject[] cogs = GameObject.FindGameObjectsWithTag("Cogumelo");
        if (cogs.Length > 0)
        {
            int indice = Random.Range(0, cogs.Length);
            cogSpd = cogs[indice];
        }
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(h * vel, rb.linearVelocity.y);

   
        if (h > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (h < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        anim.SetBool("andar", h != 0);
        anim.SetBool("noAr", !noChao);

        if (Input.GetButtonDown("Jump") && noChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, pulo);
            noChao = false;
        }

        if (transform.position.y < posInicY - 4f)
        {
            FimJogo();
        }
    }

    void FimJogo()
    {
        if (txtOver != null) txtOver.gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        rb.simulated = false;
        this.enabled = false;
    }

    void AtuTxt()
    {
        if (txtPts != null) txtPts.text = "Pontos: " + pts;
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Portal"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (outro.CompareTag("Cogumelo"))
        {
            pts++;
            AtuTxt();
            if (outro.gameObject == cogSpd)
            {
                vel = 25f;
                Invoke("ResetVel", 2f);
            }
            Destroy(outro.gameObject);
        }

        if (outro.CompareTag("Buraco"))
        {
            FimJogo();
        }

        if (outro.CompareTag("Inimigo"))
        {
            if (transform.position.y > outro.transform.position.y + 0.5f)
            {
                pts++;
                AtuTxt();
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, pulo * 0.5f);
                Destroy(outro.gameObject);
            }
            else
            {
                FimJogo();
            }
        }
    }

    void ResetVel()
    {
        vel = velOrig;
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