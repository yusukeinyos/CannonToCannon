using UnityEngine;
using System.Collections;

public class CloudMaker : MonoBehaviour
{

    public int cloudNum;
    public float yMax;
    public float yMin;
    public GameObject cloud;

    private float time;
    private float x;
    private float z;

    // Use this for initialization
    void Start()
    {
        x = transform.position.x;
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 4)
        {
            time += Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < cloudNum; i++)
            {
                switch(Random.Range(0,3))
                {
                    case 0:
                        cloud.transform.localScale = new Vector3(3, 3, 1);
                        break;
                    case 1:
                        cloud.transform.localScale = new Vector3(5, 5, 1);
                        break;
                    case 2:
                        cloud.transform.localScale = new Vector3(7, 7, 1);
                        break;
                }
                Instantiate(cloud, new Vector3(x, Random.Range(yMin, yMax), z), Quaternion.identity);
            }
            time = 0;
        }
    }
}
