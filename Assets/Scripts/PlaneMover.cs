using UnityEngine;
using System.Collections;

public class PlaneMover : MonoBehaviour
{

    public bool canOperate;
    public float speed;
    public float maxTime;
    public GameObject explosionParticle;

    private Rigidbody2D rig;
    private Animator ani;
    private float x;
    private float y;
    private float time;

    // Use this for initialization
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        canOperate = false;
        x = transform.position.x;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (canOperate)
        {
            ani.SetTrigger("FLYING");
            time += Time.deltaTime;
            if (time < maxTime)
            {
                Move();
            }
            else
            {
                //重力追加
                rig.gravityScale = 2;
                Destroy(gameObject);
                GameObject.FindWithTag("GameController").SendMessage("GameOver");
            }
        }
    }

    void Move()
    {
        int count = Input.touchCount;
        //x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        x += 1f * Time.deltaTime * speed;
        while (count == 1)
        //while (Input.GetButtonDown("Fire1"))
        {
            y += 1f * Time.deltaTime * speed;
            count = 0;
        }
        //y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position = new Vector2(x, y);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Cannon")
        {
            GameObject.FindWithTag("Player").SendMessage("ParentDelete");
            GameObject.FindWithTag("Player").SendMessage("SetIsFlying", false);
            GameObject.FindWithTag("Player").SendMessage("SetCorrectPosition", col.transform.position);

            Destroy(gameObject);
        }
        else if (col.tag == "Wall")
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position + new Vector3(0, 0, -3), Quaternion.identity);
            GameObject.FindWithTag("GameController").SendMessage("GameOver");
        }
    }
}
