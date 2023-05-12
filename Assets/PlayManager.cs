using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] BallController ballController;
    [SerializeField] CameraController camController;

    bool isBallOutside;
    bool isBallTeleporting;
    bool isBallGoal;
    Vector3 lastBallPosition;

    private void Update()
    {
        if (ballController.ShootingMode)
        {
            lastBallPosition = ballController.transform.position;
        }

        var inputActive = Input.GetMouseButton(0)
        && ballController.IsMoving() == false
        && ballController.ShootingMode == false
        && isBallOutside == false;
        camController.SetInputActive(inputActive);

    }

    public void OnBallGoal()
    {
        isBallGoal = true;
        ballController.enabled = false;
        //TODO player win window popup
    }

    public void OnBallOutside()
    {
        if(isBallGoal)
        {
            return;
        }
        if (isBallTeleporting == false)
        {
            Invoke("TeleportBallToLastPosition", 1.5f);
        }
        isBallOutside = true;
        isBallTeleporting = true;
    }

    public void TeleportBallToLastPosition()
    {
        TeleportBall(lastBallPosition);
    }

    public void TeleportBall(Vector3 targetPosition)
    {
        var rb = ballController.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ballController.transform.position = lastBallPosition;
        rb.isKinematic = false;
        isBallOutside = false;
        isBallTeleporting = false;
    }
}
