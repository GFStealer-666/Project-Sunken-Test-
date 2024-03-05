using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] GameObject brokenBottlePrefab;
    private Rigidbody rb;
    private bool isExploding = false;    

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    


    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(brokenBottlePrefab, this.transform.position, this.transform.rotation);
        // brokenBottle.GetComponent<BrokenBottle>().StartVelocityApplication();
        Destroy(gameObject);
    }
}
