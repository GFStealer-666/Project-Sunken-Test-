using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    [SerializeField] Transform throwPoint;
    [SerializeField] float animationTime;
    [SerializeField] float throwForce;

    [SerializeField] Rigidbody throwItem;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Invoke("Throw" , animationTime);
        }
    }

    private void Throw()
    {
        Rigidbody instaniateItem = (Rigidbody)Instantiate(throwItem, throwPoint.position , throwPoint.rotation);
        instaniateItem.velocity = transform.forward * throwForce;
    }
}
