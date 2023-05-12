using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Outside : MonoBehaviour
{
    public UnityEvent OnBallEnter = new UnityEvent();
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ball"))
        {
            OnBallEnter.Invoke();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ball"))
        {
            OnBallEnter.Invoke();
        }
    }
}
