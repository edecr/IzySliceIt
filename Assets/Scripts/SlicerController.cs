using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlicerController : MonoBehaviour
{
    public float UpForce = 10f; // A for�a do impulso que ser� aplicado ao objeto
    public float MoveForce = 115f; // A for�a que ser� aplicada para mover o objeto para a direita
    public float RotationForce = 180f;

    private bool _isGrounded = false;
    private bool _isAlive = true;
    private bool _isCheckPoint = false;

    private Rigidbody _rbSlicer;

    public float gravityMultiplier = 7f; // Multiplicador de gravidade
    
    private LayerMask _groundLayer;
    private LayerMask _deadZoneLayer;
    private LayerMask _checkPointLayer;

    public event Action OnDie;
    public event Action OnCheckPoint;

    private Vector3 _lastPosition;
    private Quaternion _lastRotation;

    private void Start ()
    {
        _rbSlicer = GetComponent<Rigidbody>();
        _groundLayer = LayerMask.NameToLayer("Ground");
        _deadZoneLayer = LayerMask.NameToLayer("DeadZone");
        _checkPointLayer = LayerMask.NameToLayer("CheckPoint");
    }

    private void Update ()
    {
        Debug.Log(">>> " + _isGrounded);
        if (Input.GetMouseButtonDown(0))
        {
            ReleaseSlicer();
            ApplyImpulse();
            ApplyMoveForce();
            ApplyRotationForce();
            
        //    Debug.Log("MOUSE DOWN");
        }

            ///TODO substituir por touch, usar novo input system se der tempo.
            /*   if (Input.GetMouseButtonDown(0))
               {
                   ReleaseSlicer(); //Unstuck the Slicer from ground

                   // Apply forces to move and rotate the Slicer
                   ApplyImpulse();
                   ApplyMoveForce();
                   ApplyRotationForce();
               }*/
    }

	private void FixedUpdate ()
	{
        // Added downward gravitational force
        if (!_isGrounded)
        {
            _rbSlicer.AddForce(Vector3.down * Physics.gravity.magnitude * gravityMultiplier);
        } 
    }

	private void ReleaseSlicer ()
	{
        _rbSlicer.isKinematic = false;
     //   transform.position = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
        Invoke("SetGroundedWithDelay", 0.3f);
    }

    private void ApplyImpulse ()
    {
        // Add a impulse force to move the slicer up
        _rbSlicer.AddForce(Vector3.up * UpForce, ForceMode.Impulse);
    }

    private void ApplyMoveForce ()
    {
        // Add a force to move the slicer to the right
        _rbSlicer.AddForce(Vector3.right * MoveForce, ForceMode.Force);
    }

    private void ApplyRotationForce ()
    {
        // Add a torque to rotate the slicer to the right
        _rbSlicer.AddTorque(Vector3.back * RotationForce, ForceMode.Force);
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.layer == _groundLayer && !_isGrounded)
        {
            StuckSlicerOnGround();
        } 
        else if (other.gameObject.layer == _deadZoneLayer && _isAlive)
        {
            _isAlive = false;
            _rbSlicer.isKinematic = true;
            OnDie?.Invoke();
            Debug.Log("died");
        }
        else if (other.gameObject.layer == _checkPointLayer && !_isCheckPoint)
        {
            _isCheckPoint = true;
            _rbSlicer.isKinematic = true;
            OnCheckPoint?.Invoke();
            Debug.Log("checkpoint->nextlevel");
        }
    }

	private void StuckSlicerOnGround()
	{
        _rbSlicer.isKinematic = true;
        _isGrounded = true;
        _lastPosition = transform.position;
        _lastRotation = transform.rotation;
        Debug.Log("change position");
    }

    public void MoveToLastPosition ()
	{
        transform.position = _lastPosition;
        transform.rotation = _lastRotation;
        _isAlive = true;
    }

    private void SetGroundedWithDelay ()
	{
        _isGrounded = false;
    }
}
