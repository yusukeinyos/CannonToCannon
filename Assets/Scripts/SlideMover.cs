using UnityEngine;
using System.Collections;

public class SlideMover : MonoBehaviour
{
    public int flagMoveDirection;
    public float delta;
    

    private float registerValue;
    public float cycleTime = 3;
    private float time;

    public float moveRange = 3;

    // Use this for initialization
    void Start()
    {
        switch (flagMoveDirection)
        {
            case 0:
                registerValue = transform.position.y;
                break;
            case 1:
                registerValue = transform.position.x;
                break;
        }


    }

    // Update is called once per frame
    void Update()
    {
        switch (flagMoveDirection)
        {
            case 0:
                MoveinVerticle();
                break;
            case 1:
                MoveinHorizontal();
                break;
        }
    }

    void MoveinVerticle()
    {

        time += Time.deltaTime;

        delta = Mathf.PingPong(time * moveRange / cycleTime * 2, moveRange * 2);
        delta -= moveRange;
        transform.position = new Vector3(transform.position.x, registerValue + delta, transform.position.z);

    }
    void MoveinHorizontal()
    {
        time += Time.deltaTime;

        delta = Mathf.PingPong(time * moveRange / cycleTime * 2, moveRange * 2);
        delta -= moveRange;
        transform.position = new Vector3(registerValue + delta, transform.position.y, transform.position.z);

    }
}
