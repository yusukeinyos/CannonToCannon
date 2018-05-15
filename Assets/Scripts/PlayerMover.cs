using UnityEngine;
using System.Collections;

public enum PlayerState { STANDBY, FLYING, DEAD };
public class PlayerMover : MonoBehaviour
{
    public bool isStandby; //大砲の中にいるか（発射可能か）どうか
    public bool isFlying; //Planeに乗ってるか
    public float speed;

    public float test;
    public float hp;

    public PlayerState state;

    public GameObject burstParticle;

    private bool isHPCounting;
    private float maxHP = 1.5f;
    private Rigidbody2D rig;
    public Vector2 direction = Vector2.right;
    private CannonMover can_mover;
    private PlaneMover plane_mover;

    // Use this for initialization
    void Start()
    {
        isHPCounting = false;

        state = PlayerState.STANDBY;
        isFlying = false;
        rig = GetComponent<Rigidbody2D>();
    }

    public void Init()
    {
        rig.velocity = speed * Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        //if (Input.touchCount == 1)
        {
            isHPCounting = true;
            hp = 0;
            Shot();

        }

        if (isHPCounting)
        {
            if (hp > maxHP)
            {
                Explosion();
            }
            else
            {
                hp += Time.deltaTime;
            }
        }

    }

    void Shot()
    {
        //if (isStandby)
        if (state == PlayerState.STANDBY)
        {
            ParentDelete();
            rig.velocity = speed * direction;
            if (can_mover != null)
                can_mover.isPlayerIn = false;
        }
    }

    public void SetDirection(Vector2 vec)
    {
        direction = vec;
        int deg = (int)Vector2.Angle(Vector2.right, direction);
        test = deg;
        switch ((deg + 360) % 360)
        {
            case 0:
                transform.rotation = Quaternion.Euler(0, 0, 0 - 90f);
                break;
            case 90:
                transform.rotation = Quaternion.Euler(0, 0, 90f - 90f);
                break;
            case 180:
                transform.rotation = Quaternion.Euler(0, 0, 180f - 90f);
                break;
            case 270:
                transform.rotation = Quaternion.Euler(0, 0, 270f - 90f);
                break;
        }
    }

    public void SetHP()
    {
        isHPCounting = false;
        hp = 0;
    }

    public void SetCorrectPosition(Vector3 vec)
    {
        transform.position = vec;
        SetIsStandBy(true);
    }

    public void SetIsStandBy(bool flag)
    {
        isStandby = flag;
    }

    public void SetIsFlying(bool flag)
    {
        isFlying = flag;
    }

    public void ParentDelete()
    {
        if (transform.parent != null)
            transform.parent = null;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Cannon")
        {
            if (!isFlying)
            {
                SetHP();
                rig.velocity = Vector2.zero;
                transform.parent = col.gameObject.transform;
                transform.position = transform.parent.transform.position;
                //transform.position = col.transform.position;
                can_mover = col.gameObject.GetComponent<CannonMover>();
            }
        }
        else if (col.tag == "Plane")
        {
            SetHP();
            rig.velocity = Vector2.zero;
            transform.parent = col.gameObject.transform;
            transform.position = transform.parent.transform.position;
            plane_mover = col.gameObject.GetComponent<PlaneMover>();
            plane_mover.canOperate = true;
            isFlying = true;
            isStandby = false;
        }
        else if (col.tag == "Item")
        {
            GameObject.FindWithTag("GameController").SendMessage("AddScore", 10);
            Destroy(col.gameObject);
        }
        else if (col.tag == "Goal")
        {
            rig.velocity = Vector2.zero;
            GameObject.FindWithTag("GameController").SendMessage("Goal");
        }

    }

    public void Explosion()
    {
        GameObject particle = Instantiate(burstParticle, transform.position + new Vector3(0, 0, -3), Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(particle, 2);
        GameObject.FindWithTag("GameController").SendMessage("GameOver");
    }
}
