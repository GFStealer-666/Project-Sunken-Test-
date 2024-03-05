using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBottle : MonoBehaviour
{
    [SerializeField] GameObject[] pieces;
    [SerializeField] float velMultiplier = 2f;
    [SerializeField] float timeBeforeDestroying = 1f;
    [SerializeField] float velocityApplyInterval = 0.05f;
    [SerializeField] AudioSource audioSource;
    private int currentIndex = 0;
    void Start()
    {
        audioSource.Play();
        Destroy(this.gameObject, timeBeforeDestroying);
        
    }

    // public void StartVelocityApplication()
    // {
    //     StartCoroutine(ApplyVelocitiesOverTime());
    // }
    
    // IEnumerator ApplyVelocitiesOverTime()
    // {
    //     while (currentIndex < pieces.Length)
    //     {
    //         ApplyRandomVelocity();
    //         yield return new WaitForSeconds(velocityApplyInterval);
    //     }
    // }

    // void ApplyRandomVelocity()
    // {
    //     if (currentIndex < pieces.Length)
    //     {
    //         float xVel = UnityEngine.Random.Range(0f, 1f);
    //         float yVel = UnityEngine.Random.Range(0f, 1f);
    //         float zVel = UnityEngine.Random.Range(0f, 1f);
    //         Vector3 vel = new Vector3(velMultiplier * xVel, velMultiplier * yVel, velMultiplier * zVel);
    //         pieces[currentIndex].GetComponent<Rigidbody>().velocity = vel;
    //         currentIndex++;
    //     }
    // }
}