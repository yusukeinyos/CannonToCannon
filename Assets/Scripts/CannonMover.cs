using UnityEngine;
using System.Collections;

public class CannonMover : MonoBehaviour
{

    private float startWaitTime = 3.0f;
    private float rotateWaitTime = 2.0f;
    private Vector2 directionVector;
    private Animator ani;

    public bool isPlayerIn;

    public bool isMovable;
    public int directionFlag; //0:right 1:up 2:left 3:down 初期方向
    public bool clockWise; //回転方向
    public bool isDead;

    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        isDead = false;
        switch (directionFlag)
        {
            case 0:
                directionVector = Vector2.right;
                break;
            case 1:
                directionVector = Vector2.up;
                break;
            case 2:
                directionVector = Vector2.left;
                break;
            case 3:
                directionVector = Vector2.down;
                break;
        }

        StartCoroutine("DescreteRotation");
    }



    IEnumerator DescreteRotation()
    {
        yield return new WaitForSeconds(startWaitTime);
        while (true)
        {
            Rotate();
            if (isDead)
                break;
            yield return new WaitForSeconds(rotateWaitTime);
        }
    }

    void Rotate()
    {
        if (isMovable)
        {
            if (clockWise)
            {
                transform.Rotate(Vector3.forward, -90f);
                directionVector = RotateVectorByAngle(directionVector, 270f);
            }
            else
            {
                transform.Rotate(Vector3.forward, 90f);
                directionVector = RotateVectorByAngle(directionVector, 90f);
            }
        }
        if (isPlayerIn)
        {
            GameObject.FindWithTag("Player").SendMessage("SetDirection", directionVector);
            GameObject.FindWithTag("Player").SendMessage("SetHP");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" || col.tag == "Plane")
        {
            isPlayerIn = true;
            ani.SetTrigger("INTO");
        }
    }

    Vector2 RotateVectorByAngle(Vector2 vec, float deg)
    {
        float rad = Mathf.PI * deg / 180f;
        int x = (int)Vector2.Dot(new Vector2(Mathf.Cos(rad), -Mathf.Sin(rad)), vec);
        int y = (int)Vector2.Dot(new Vector2(Mathf.Sin(rad), Mathf.Cos(rad)), vec);
        return new Vector2(x, y);
    }
}
