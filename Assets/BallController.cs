using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Collider col;
    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    //[SerializeField] LayerMask layerMask;
    [SerializeField] LineRenderer aimLine;
    [SerializeField] Transform aimWorld;
    bool shoot;
    bool shootingMode;
    float forceFactor;
    Ray ray;
    Plane plane;
    Vector3 forceDirection;

    public bool ShootingMode { get => shootingMode; }

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
        if (ShootingMode)
        {
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

                // ui aim line
                // aimLine.transform.position = this.transform.position;
                // var positions = new Vector3[] { ballScreenPos, Input.mousePosition };
                // aimLine.SetPositions(positions);

                // force direction
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                plane.Raycast(ray, out var distance);
                forceDirection = (this.transform.position - ray.GetPoint(distance));
                forceDirection.Normalize();

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
            rb.AddForce(forceDirection * force * forceFactor, ForceMode.Impulse);
        }

        if (rb.velocity.sqrMagnitude < 0.01f && rb.velocity.sqrMagnitude > 0)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public bool IsMoving()
    {
        return rb.velocity != Vector3.zero;
    }

}
