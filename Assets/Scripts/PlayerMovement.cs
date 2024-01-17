using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float _speed = 5;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    public Rigidbody _rb;

    float horizontalInput;
    public float horizontalMultiplier = 2;


    private int _numberOfColliderUnder = 0;

    private void FixedUpdate()
    {

        Vector3 forwardMove = transform.forward * _speed * Time.deltaTime;
        Vector3 horizontaleMove = transform.right * horizontalInput * _speed * Time.fixedDeltaTime * horizontalMultiplier;
        _rb.MovePosition(_rb.position + forwardMove + horizontaleMove);

        if (Input.GetKeyDown(KeyCode.Space) /*&& _numberOfColliderUnder > 0*/)
        {
            _rb.AddForce(new Vector3(0, _jumpForce, 0));
        }


        if (_rb.velocity.y < -1)
            _rb.AddForce(Physics.gravity * Time.deltaTime * 2);

    }


    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
    }


    private void OnTriggerEnter(Collider other)
    {
        _numberOfColliderUnder++;
    }
    private void OnTriggerExit(Collider other)
    {
        _numberOfColliderUnder--;

    }


}
