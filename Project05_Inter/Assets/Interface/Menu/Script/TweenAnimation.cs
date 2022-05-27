using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween 
{ 
    public class TweenAnimation : MonoBehaviour
    {
        [SerializeField]
        private float m_AnimationTime = 0.3f;
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
                percentageComplete = enlapsedTime / m_AnimationTime;

                transform.SetPositionAndRotation(Vector3.Lerp(InitialPosition, TargetPosition, Mathf.SmoothStep(0, 1, percentageComplete)), Quaternion.Lerp(InitialRotation, TargetRotation, Mathf.SmoothStep(0, 1, percentageComplete)));
                transform.localScale = Vector3.Lerp(InitialScale, TargetScale, Mathf.SmoothStep(0, 1, percentageComplete));

                if(conditionToEnd)
                {
                    InitialPosition = Vector3.zero;
                    TargetPosition = Vector3.zero;
                    InitialScale = Vector3.zero;
                    TargetScale = Vector3.zero;
                    enlapsedTime = 0f;
                    doTween = false;
                }
            }
        }

        public void StartMovement(Transform desiredPos)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = desiredPos.position;
            TargetRotation = transform.rotation;
            TargetScale = transform.localScale;

            enlapsedTime = 0f;
            doTween = true;
            conditionToEnd = transform.position == TargetPosition;
        }

        public void StartRotation(Transform desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = transform.position;
            TargetRotation = desiredRot.rotation;
            TargetScale = transform.localScale;

            enlapsedTime = 0f;
            doTween = true;
            conditionToEnd = transform.rotation == TargetRotation;
        }

        public void StartScale(Transform desiredScale)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = transform.position;
            TargetRotation = transform.rotation;
            TargetScale = desiredScale.localScale;

            enlapsedTime = 0f;
            doTween = true;
            conditionToEnd = transform.localScale == TargetScale;
        }

        public void StartMovementAndRotation(Vector3 desiredPos, Quaternion desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = desiredPos;
            TargetRotation = desiredRot;
            TargetScale = transform.localScale;

            enlapsedTime = 0f;
            doTween = true;
            conditionToEnd = transform.position == TargetPosition && transform.rotation == TargetRotation;
        }

        public void StartMovementAndScale(Vector3 desiredPos, Vector3 desiredScale)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = desiredPos;
            TargetRotation = transform.rotation;
            TargetScale = desiredScale;

            enlapsedTime = 0f;
            doTween = true;
            conditionToEnd = transform.position == TargetPosition && transform.localScale == TargetScale;
        }

        public void StartRotationAndScale(Vector3 desiredScale, Quaternion desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = transform.position;
            TargetRotation = desiredRot;
            TargetScale = desiredScale;

            enlapsedTime = 0f;
            doTween = true;
            conditionToEnd = transform.localScale == TargetScale && transform.rotation == TargetRotation;
        }

        public void StartMovementRotationAndScale(Vector3 desiredPos, Vector3 desiredScale, Quaternion desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = desiredPos;
            TargetRotation = desiredRot;
            TargetScale = desiredScale;

            enlapsedTime = 0f;
            doTween = true;
            conditionToEnd = transform.position == TargetPosition && transform.localScale == TargetScale && transform.rotation == TargetRotation;
        }
    }
}