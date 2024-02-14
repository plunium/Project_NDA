using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float _forwardSpeed;
    [SerializeField]
    private float _lateralSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _acceleration = 10;
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private Animator _playerAnimator;

    private float _timer = 0;
    private int _numberOfColliderUnder = 0;

    void Update()
    {
        _timer += Time.deltaTime;

        float forwardDelta = _forwardSpeed * Time.deltaTime;
        float lateralDelta = _lateralSpeed * Time.deltaTime;

        Vector3 CurrentSpeed = _rb.velocity;

        Vector3 tempSpeed = CurrentSpeed;

        //mouvement avant
        tempSpeed = transform.forward * _forwardSpeed;

        //On conserve la vitesse verticale
        tempSpeed.y = _rb.velocity.y;

        // Mouvement horizontal
        if (Input.GetKey(KeyCode.RightArrow))
            tempSpeed += transform.right * _lateralSpeed;
        if (Input.GetKey(KeyCode.LeftArrow))
            tempSpeed += -transform.right * _lateralSpeed;

        _rb.velocity = Vector3.Lerp(_rb.velocity, tempSpeed, _acceleration * Time.deltaTime);

        //Mouvement de saut
        if (Input.GetKeyDown(KeyCode.Space) && _numberOfColliderUnder > 0 && _timer > 1.0f)
        {
            _rb.AddForce(new Vector3(0, _jumpForce, 0));
            _playerAnimator.SetTrigger("Jump");
        }

        //Mouvement de slide
        // en QWERTY Z = W
        if (Input.GetKeyDown(KeyCode.Z) && _numberOfColliderUnder > 0 && _timer > 1.0f)
        {
            _playerAnimator.SetTrigger("Slide");
        }

        //Gravité
        if (_rb.velocity.y < -1)
            _rb.AddForce(Physics.gravity * Time.deltaTime * 100);

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
