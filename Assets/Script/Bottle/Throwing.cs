using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Throwing : MonoBehaviour
{
    [SerializeField] Transform throwPoint;
    [SerializeField] Transform parentPoint;
    [SerializeField] float animationTime;
    [SerializeField] float throwForce , throwArcStrength = 0.5f;

    [SerializeField] GameObject throwItem;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Invoke("Throw" , animationTime);
        }
    }

    private void Throw()
    {
        GameObject instaniateItem = Instantiate(throwItem , throwPoint.position ,parentPoint.rotation);
        Rigidbody instaniateRb = instaniateItem.GetComponent<Rigidbody>();
        Vector3 throwDirection = parentPoint.forward * throwArcStrength;
        Debug.Log(throwDirection.normalized);
        instaniateRb.velocity = throwDirection.normalized * throwForce;
    }
}
