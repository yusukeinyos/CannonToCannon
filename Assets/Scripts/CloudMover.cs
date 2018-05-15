using UnityEngine;
using System.Collections;

public class CloudMover : MonoBehaviour
{
    private float minX;
    private float speed;
    private float maaxTime = 10;
    private float time;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        int flag=Random.Range(0, 3);
        switch (flag)
        {
            case 0:
                speed = 3;
                break;
            case 1:
                speed = 7;
                break;
            case 2:
                speed = 15;
                break;
        }
        transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, transform.position.y, transform.position.z);
        if (time > maaxTime)
            Destroy(gameObject);
    }
}
