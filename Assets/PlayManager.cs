using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] BallController ballController;
    [SerializeField] CameraController camController;
    [SerializeField] GameObject finishWindow;
    [SerializeField] TMP_Text finishText;
    [SerializeField] TMP_Text shootCountText;

    bool isBallOutside;
    bool isBallTeleporting;
    bool isBallGoal;
    Vector3 lastBallPosition;

    private void OnEnable()
    {
        ballController.onBallShooted.AddListener(UpdateShootCount);
    }

    private void OnDisable()
    {
        ballController.onBallShooted.RemoveListener(UpdateShootCount);
    }

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
        Debug.Log("Input Active: " + inputActive + " Is Moving: " + ballController.IsMoving() + " Shooting Mode: " + ballController.ShootingMode + " Is Ball Outside: " + isBallOutside);
        camController.SetInputActive(inputActive);

    }

    public void OnBallGoal()
    {
        isBallGoal = true;
        ballController.enabled = false;

        finishWindow.gameObject.SetActive(true);
        finishText.text = "Level Complete!\n" + "Shoot Count: " + ballController.ShootCount;
    }

    public void OnBallOutside()
    {
        if (isBallGoal)
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

    public void UpdateShootCount(int shootCount)
    {
        shootCountText.text = "Shoot Count: " + shootCount;
    }
}
