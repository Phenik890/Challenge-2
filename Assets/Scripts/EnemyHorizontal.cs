using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : MonoBehaviour
{

    public float speed = 3f;
    bool movement = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movement)
        {
            moveRight();
        }
        if (!movement)
        {
            moveLeft();
        }
        if (transform.position.x >= 18f)
        {
            movement = false;
        }
        if (transform.position.x <= 10f)
        {
            movement = true;
        }
    }

    void moveRight()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
    void moveLeft()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
}
