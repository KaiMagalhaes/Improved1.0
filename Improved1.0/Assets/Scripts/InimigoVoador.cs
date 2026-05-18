using UnityEngine;

public class InimigoVoador : MonoBehaviour
{
    public float vel = 3f;
    private Vector3 posInic;

    void Start()
    {
        posInic = transform.position;
    }

    void Update()
    {
        float novoY = posInic.y + Mathf.Sin(Time.time * vel) * 0.4f;
        transform.position = new Vector3(transform.position.x, novoY, transform.position.z);
    }
}