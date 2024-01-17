using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    public float _runningSpeed;
    [SerializeField]
    public float _horizontalSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField] 
    private float _jumpSpeed;
    [SerializeField]
    public Rigidbody _rb;


    float horizontalInput;
    //public float _horizontalMultiplier = 5;

    private int _numberOfColliderUnder = 0;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        //float speedDelta = _forwardSpeed * Time.deltaTime;

        Vector3 forwardMove = transform.forward * _runningSpeed * Time.deltaTime;
        //Vector3 horizontalSpeed = transform.right * horizontalInput * _horizontalSpeed * Time.deltaTime;

        Vector3 horizontalMove = Vector3.zero;

        if (Input.GetKey(KeyCode.RightArrow))
            horizontalMove = transform.right * _horizontalSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
            horizontalMove = -transform.right * _horizontalSpeed * Time.deltaTime;


        _rb.velocity = (forwardMove + horizontalMove);

        if (Input.GetKeyDown(KeyCode.Space) && _numberOfColliderUnder > 0)
        {
            _rb.AddForce(new Vector3(0, _jumpForce, 0));
        }

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
