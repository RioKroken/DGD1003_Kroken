using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rigid;

    [SerializeField]
    private float speed;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Movement(horizontal);

    }


    void Movement(float horizontal)
    {
        rigid.linearVelocity = new Vector2(horizontal*speed,rigid.linearVelocity.y);

    }
}
