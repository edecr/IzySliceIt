using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableObject : MonoBehaviour
{
    //[SerializeField]
    //private Material _material;

    private MeshRenderer _mesh;
    private Collider _collider;
    private Rigidbody _rbChild1;
    private Rigidbody _rbChild2;

    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _rbChild1 = transform.GetChild(0).GetComponent<Rigidbody>();
        _rbChild2 = transform.GetChild(1).GetComponent<Rigidbody>();

     // Setup meshes materials
    //  _mesh.material = _material;
     // transform.GetChild(0).GetComponent<MeshRenderer>().material = _material;
     // transform.GetChild(1).GetComponent<MeshRenderer>().material = _material;
    }

    private void OnTriggerEnter (Collider other)
    {
    //    Debug.Log("entrouu");
        if (other.gameObject.layer == LayerMask.NameToLayer("Slicer"))
        {
            // Hide main object
            _mesh.enabled = false;
            _collider.enabled = false;

            // Enables physics of the sliced objects and apply force impulse
            _rbChild1.isKinematic = false;
            _rbChild1.AddForce(-_rbChild1.transform.forward * 7.0f, ForceMode.Impulse);
            _rbChild2.isKinematic = false;
            _rbChild2.AddForce(_rbChild1.transform.forward * 7.0f, ForceMode.Impulse);

            GameManager.Instance.SetScore();
        }
    }
}
