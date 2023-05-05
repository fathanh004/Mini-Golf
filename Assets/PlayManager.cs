using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] BallController ballController;
    [SerializeField] CameraController camController;
    private void Update()
    {
        var inputActive = Input.GetMouseButton(0) && ballController.IsMoving() == false;
        camController.SetInputActive(inputActive);
    }
}
