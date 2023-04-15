using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerController : MonoBehaviour
{
    public float UpForce = 10f; // A força do impulso que será aplicado ao objeto
    public float MoveForce = 115f; // A força que será aplicada para mover o objeto para a direita
    public float RotationForce = 180f;

    private Rigidbody _rbSlicer;

    private void Start ()
    {
        _rbSlicer = GetComponent<Rigidbody>();
    }

    private void Update ()
    {
        ///TODO substituir por touch, usar novo input system se der tempo.
        if (Input.GetMouseButtonDown(0))
        {
            ReleaseSlicer(); //Unstuck the Slicer from ground

            // Apply forces to move and rotate the Slicer
            ApplyImpulse();
            ApplyMoveForce();
            ApplyRotationForce();
        }
    }

    private void ReleaseSlicer ()
	{
        _rbSlicer.isKinematic = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StuckSlicerOnGround();
        }
    }
    /* ADICIONAR NO SCRIPT DO OBJETO Q SERÁ CORTADO
     private void OnTriggerEnter (Collider other)
	{
        Debug.Log("entrouu");
        other.GetComponent<MeshRenderer>().enabled = false;
        other.GetComponent<BoxCollider>().enabled = false;
        Rigidbody tempRb = other.GetComponentInChildren<Rigidbody>();
        tempRb.isKinematic = false;
        tempRb.AddForce(-transform.forward * 10.0f, ForceMode.Impulse);
     //   tempRb.AddForceAtPosition(-transform.forward * 10.0f, Vector3.right * 2f, ForceMode.Impulse);

    }
     */
    private void StuckSlicerOnGround()
	{
        _rbSlicer.isKinematic = true;
        
        if (transform.eulerAngles.z < 50)
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.12f, transform.position.z);
        else if (transform.eulerAngles.z < 120)
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
        else if (transform.eulerAngles.z < 230)
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.12f, transform.position.z);
        else if(transform.eulerAngles.z < 300)
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.12f, transform.position.z);
    }
}
