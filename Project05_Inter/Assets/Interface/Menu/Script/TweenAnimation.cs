using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween 
{ 
    public class TweenAnimation : MonoBehaviour
    {
        private float desiredTime = 0f;
        private float enlapsedTime = 0f;
        private float percentageComplete = 0f;
        private bool doTween = false;
        private bool conditionToEnd;

        private Vector3 InitialPosition;
        private Vector3 InitialScale;
        private Quaternion InitialRotation;

        private Vector3 TargetPosition;
        private Vector3 TargetScale;
        private Quaternion TargetRotation;

        private void Update()
        {
            DoTween();
        }

        private void DoTween()
        {
            if (doTween)
            {
                enlapsedTime += Time.deltaTime;
                percentageComplete = enlapsedTime / desiredTime;

                transform.SetPositionAndRotation(Vector3.Lerp(InitialPosition, TargetPosition, Mathf.SmoothStep(0, 1, percentageComplete)), Quaternion.Lerp(InitialRotation, TargetRotation, Mathf.SmoothStep(0, 1, percentageComplete)));
                transform.localScale = Vector3.Lerp(InitialScale, TargetScale, Mathf.SmoothStep(0, 1, percentageComplete));

                if(conditionToEnd)
                {
                    InitialPosition = Vector3.zero;
                    TargetPosition = Vector3.zero;
                    InitialScale = Vector3.zero;
                    TargetScale = Vector3.zero;
                    enlapsedTime = 0f;
                    desiredTime = 0f;
                    doTween = false;
                }
            }
        }

        public void StartMovement(float timeInSeconds, Vector3 desiredPos)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = desiredPos;
            TargetRotation = transform.rotation;
            TargetScale = transform.localScale;

            enlapsedTime = 0f;
            desiredTime = timeInSeconds;
            doTween = true;
            conditionToEnd = transform.position == TargetPosition;
        }

        public void StartRotation(float timeInSeconds, Quaternion desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = transform.position;
            TargetRotation = desiredRot;
            TargetScale = transform.localScale;

            enlapsedTime = 0f;
            desiredTime = timeInSeconds;
            doTween = true;
            conditionToEnd = transform.rotation == TargetRotation;
        }

        public void StartScale(float timeInSeconds, Vector3 desiredScale)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = transform.position;
            TargetRotation = transform.rotation;
            TargetScale = desiredScale;

            enlapsedTime = 0f;
            desiredTime = timeInSeconds;
            doTween = true;
            conditionToEnd = transform.localScale == TargetScale;
        }

        public void StartMovementAndRotation(float timeInSeconds, Vector3 desiredPos, Quaternion desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = desiredPos;
            TargetRotation = desiredRot;
            TargetScale = transform.localScale;

            enlapsedTime = 0f;
            desiredTime = timeInSeconds;
            doTween = true;
            conditionToEnd = transform.position == TargetPosition && transform.rotation == TargetRotation;
        }

        public void StartMovementAndScale(float timeInSeconds, Vector3 desiredPos, Vector3 desiredScale)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = desiredPos;
            TargetRotation = transform.rotation;
            TargetScale = desiredScale;

            enlapsedTime = 0f;
            desiredTime = timeInSeconds;
            doTween = true;
            conditionToEnd = transform.position == TargetPosition && transform.localScale == TargetScale;
        }

        public void StartRotationAndScale(float timeInSeconds, Vector3 desiredScale, Quaternion desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = transform.position;
            TargetRotation = desiredRot;
            TargetScale = desiredScale;

            enlapsedTime = 0f;
            desiredTime = timeInSeconds;
            doTween = true;
            conditionToEnd = transform.localScale == TargetScale && transform.rotation == TargetRotation;
        }

        public void StartMovementRotationAndScale(float timeInSeconds, Vector3 desiredPos, Vector3 desiredScale, Quaternion desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = desiredPos;
            TargetRotation = desiredRot;
            TargetScale = desiredScale;

            enlapsedTime = 0f;
            desiredTime = timeInSeconds;
            doTween = true;
            conditionToEnd = transform.position == TargetPosition && transform.localScale == TargetScale && transform.rotation == TargetRotation;
        }
    }
}