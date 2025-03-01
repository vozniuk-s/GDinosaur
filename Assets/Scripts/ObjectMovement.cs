using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    void Update()
    {
        transform.position = new Vector2(transform.position.x - 1 * speed * Time.deltaTime, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EndWall")
            Destroy(gameObject);
    }
}
