using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BallController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Collider col;
    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    //[SerializeField] LayerMask layerMask;
    [SerializeField] LineRenderer aimLine;
    [SerializeField] Transform aimWorld;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject shootableIndicator;
    bool shoot;
    bool shootingMode;
    float forceFactor;
    Ray ray;
    Plane plane;
    Vector3 forceDirection;

    int shootCount;

    public bool ShootingMode { get => shootingMode; }
    public int ShootCount { get => shootCount; }

    public UnityEvent<int> onBallShooted = new UnityEvent<int>();

    private void Update()
    {
        /* manual raycast
         if (Input.GetMouseButtonDown(0))
         {
             var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             if (Physics.Raycast(ray, out var hitInfo, 100, layerMask) && hitInfo.collider == col)
             {
                shoot = true;
            }
         } 
        */


        if (shootingMode)
        {
            if (Input.GetMouseButtonDown(1))
            {
                shootingMode = false;
                aimLine.gameObject.SetActive(false);
                aimWorld.gameObject.SetActive(false);
            }
            if (Input.GetMouseButtonDown(0))
            {
                aimLine.gameObject.SetActive(true);
                aimWorld.gameObject.SetActive(true);
                plane = new Plane(Vector3.up, this.transform.position);
            }
            else if (Input.GetMouseButton(0))
            {
                var mouseViewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                var ballViewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
                var ballScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
                var pointerDirection = ballViewportPos - mouseViewportPos;
                pointerDirection.z = 0;
                pointerDirection.z *= Camera.main.aspect;
                pointerDirection.z = Mathf.Clamp(pointerDirection.z, -0.5f, 0.5f);

                // ui aim line
                var mouseScreenPos = Input.mousePosition;
                ballScreenPos.z = 1f;
                mouseScreenPos.z = 1f;
                var positions = new Vector3[]
                {
                    Camera.main.ScreenToWorldPoint(ballScreenPos),
                    Camera.main.ScreenToWorldPoint(mouseScreenPos)
                };
                aimLine.SetPositions(positions);
                aimLine.endColor = Color.Lerp(Color.green, Color.red, forceFactor);

                // force direction
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                plane.Raycast(ray, out var distance);
                forceDirection = (this.transform.position - ray.GetPoint(distance));
                forceDirection.Normalize();
                Debug.Log(forceDirection);

                //force factor
                forceFactor = pointerDirection.magnitude * 2;

                //aim direction visuals
                aimWorld.transform.position = this.transform.position;
                aimWorld.forward = forceDirection;
                aimWorld.localScale = new Vector3(1, 1, 0.5f + forceFactor);

            }
            else if (Input.GetMouseButtonUp(0))
            {
                shoot = true;
                shootingMode = false;
                aimLine.gameObject.SetActive(false);
                aimWorld.gameObject.SetActive(false);
                // shootableIndicator.SetActive(false);
            }
        }
    }

    //Physics Raycast
    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.IsMoving())
        {
            return;
        }
        shootingMode = true;
    }

    private void FixedUpdate()
    {
        if (shoot)
        {
            shoot = false;

            AddForce(forceDirection * force * forceFactor, ForceMode.Impulse);
            shootCount++;
            onBallShooted.Invoke(shootCount);
        }

        if (rb.velocity.sqrMagnitude < 0.01f && rb.velocity.sqrMagnitude > 0)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            // if (!IsMoving())
            // {
            //     shootableIndicator.SetActive(true);
            // }
            // else
            // {
            //     shootableIndicator.SetActive(false);
            // }

        }


    }

    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Impulse)
    {
        rb.AddForce(force, forceMode);
    }

    public bool IsMoving()
    {
        return rb.velocity.sqrMagnitude > 0.01f;
    }

    public void Respawn()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        this.transform.position = spawnPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            //onHit.Invoke();
            Respawn();
        }
    }
}
