using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : CameraStateMachine
{
    public string PreviousCameraState { get; private set; }
    public string CurrentCameraState { get; private set; }
    private Animator _anim;
    private CinemachineStateDrivenCamera _cameraStates;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _cameraStates = GetComponent<CinemachineStateDrivenCamera>();
    }

    public void ChangeCamera(string newCameraState)
    {
        if(newCameraState != CurrentCameraState)
        {
            PreviousCameraState = CurrentCameraState;
            CurrentCameraState = newCameraState;

            _anim.Play(newCameraState);

            for (int i = 0; i < _cameraStates.ChildCameras.Length; i++)
            {
                if (_cameraStates.ChildCameras[i].name == "CM vCam_" + newCameraState) _cameraStates.ChildCameras[i].Priority = 1;
                else _cameraStates.ChildCameras[i].Priority = 0;
            }
        }
    }
}