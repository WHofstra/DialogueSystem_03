using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _speed *= 10.0f;
    }

    void FixedUpdate()
    {
        float velX = Input.GetAxis(Constants.Input.HORIZONTAL) * _speed * Time.deltaTime;
        float velY = Input.GetAxis(Constants.Input.VERTICAL) * _speed * Time.deltaTime;
        rb.velocity = new Vector2(velX, velY);
    }
}
