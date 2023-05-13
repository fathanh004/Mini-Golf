using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed; //Kecepatan proyektil
    public Transform target; //Target yang akan ditembak


    // Update is called once per frame
    private void Start()
    {
        transform.LookAt(target);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); //Menggerakkan proyektil ke depan
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            return;
        }
        Destroy(this.gameObject);
    }
    
}
 