using System;
using UnityEngine;

public class DinosaurMovement : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 12.0f;
    [SerializeField] private float jumpHeight = 5.0f;

    public static event Action OnPlayerHit;
    private bool _isGrounded = true;
    private Vector2 targetPosition;
    private Vector2 startPosition;

    void Update()
    {
        if( !_isGrounded)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, jumpSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
                targetPosition = startPosition;

            if (Vector2.Distance(transform.position, startPosition) < 0.1f && targetPosition == startPosition)
                _isGrounded = true;
        }

        if (Input.GetMouseButtonDown(0) && _isGrounded)
        {
            _isGrounded = false;
            startPosition = transform.position;
            targetPosition = new Vector2(transform.position.x, transform.position.y + jumpHeight);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            OnPlayerHit?.Invoke();
        }
    }
}
