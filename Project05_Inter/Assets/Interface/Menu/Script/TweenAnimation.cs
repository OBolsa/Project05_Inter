using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tween 
{ 
    public class TweenAnimation : MonoBehaviour
    {
        [SerializeField]
        private float m_MoveAnimationTime = 0.3f;
        private float moveEnlapsedTime = 0f;
        private float movePercentageComplete = 0f;
        private bool doMoveTween = false;
        private bool conditionToEndMove;

        private Vector3 InitialPosition;
        private Vector3 InitialScale;
        private Quaternion InitialRotation;

        private Vector3 TargetPosition;
        private Vector3 TargetScale;
        private Quaternion TargetRotation;

        private float fadeAnimationTime;
        private float fadeEnlapsedTime = 0f;
        private float fadePercentageComplete = 0f;
        private bool doFadeTween = false;
        private bool fadeIn;
        private bool conditionToEndFade;

        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private UnityEvent OnStartFadeIn;
        [SerializeField]
        private UnityEvent OnStartFadeOut;
        [SerializeField]
        private UnityEvent OnEndFadeIn;
        [SerializeField]
        private UnityEvent OnEndFadeOut;

        private void Update()
        {
            DoMovementTween();
            DoFadeTween();
        }

        private void DoFadeTween()
        {
            if (doFadeTween)
            {
                fadeEnlapsedTime += Time.deltaTime;
                fadePercentageComplete = fadeEnlapsedTime / fadeAnimationTime;

                canvasGroup.alpha = fadeIn ? Mathf.SmoothStep(0, 1, fadePercentageComplete) : Mathf.SmoothStep(1, 0, fadePercentageComplete);

                if (conditionToEndFade)
                {
                    canvasGroup = null;
                    fadeEnlapsedTime = 0f;
                    doFadeTween = false;

                    if (fadeIn)
                        OnEndFadeIn?.Invoke();
                    else
                        OnEndFadeOut?.Invoke();
                }
            }
        }

        public void StartFadeIn(float fadeTime)
        {
            fadeAnimationTime = fadeTime;
            canvasGroup.alpha = 0f;
            fadeEnlapsedTime = 0f;
            conditionToEndFade = canvasGroup.alpha == 1f;
            fadeIn = true;
            doFadeTween = true;
            OnStartFadeIn?.Invoke();
        }

        public void StartFadeOut(float fadeTime)
        {
            fadeAnimationTime = fadeTime;
            canvasGroup.alpha = 1f;
            fadeEnlapsedTime = 0f;
            conditionToEndFade = canvasGroup.alpha == 0f;
            fadeIn = false;
            doFadeTween = true;
            OnStartFadeOut?.Invoke();
        }

        private void DoMovementTween()
        {
            if (doMoveTween)
            {
                moveEnlapsedTime += Time.deltaTime;
                movePercentageComplete = moveEnlapsedTime / m_MoveAnimationTime;

                transform.SetPositionAndRotation(Vector3.Lerp(InitialPosition, TargetPosition, Mathf.SmoothStep(0, 1, movePercentageComplete)), Quaternion.Lerp(InitialRotation, TargetRotation, Mathf.SmoothStep(0, 1, movePercentageComplete)));
                transform.localScale = Vector3.Lerp(InitialScale, TargetScale, Mathf.SmoothStep(0, 1, movePercentageComplete));

                if(conditionToEndMove)
                {
                    InitialPosition = Vector3.zero;
                    TargetPosition = Vector3.zero;
                    InitialScale = Vector3.zero;
                    TargetScale = Vector3.zero;
                    moveEnlapsedTime = 0f;
                    doMoveTween = false;
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

            moveEnlapsedTime = 0f;
            doMoveTween = true;
            conditionToEndMove = transform.position == TargetPosition;
        }

        public void StartRotation(Transform desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = transform.position;
            TargetRotation = desiredRot.rotation;
            TargetScale = transform.localScale;

            moveEnlapsedTime = 0f;
            doMoveTween = true;
            conditionToEndMove = transform.rotation == TargetRotation;
        }

        public void StartScale(Transform desiredScale)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = transform.position;
            TargetRotation = transform.rotation;
            TargetScale = desiredScale.localScale;

            moveEnlapsedTime = 0f;
            doMoveTween = true;
            conditionToEndMove = transform.localScale == TargetScale;
        }

        public void StartMovementAndRotation(Vector3 desiredPos, Quaternion desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = desiredPos;
            TargetRotation = desiredRot;
            TargetScale = transform.localScale;

            moveEnlapsedTime = 0f;
            doMoveTween = true;
            conditionToEndMove = transform.position == TargetPosition && transform.rotation == TargetRotation;
        }

        public void StartMovementAndScale(Vector3 desiredPos, Vector3 desiredScale)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = desiredPos;
            TargetRotation = transform.rotation;
            TargetScale = desiredScale;

            moveEnlapsedTime = 0f;
            doMoveTween = true;
            conditionToEndMove = transform.position == TargetPosition && transform.localScale == TargetScale;
        }

        public void StartRotationAndScale(Vector3 desiredScale, Quaternion desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = transform.position;
            TargetRotation = desiredRot;
            TargetScale = desiredScale;

            moveEnlapsedTime = 0f;
            doMoveTween = true;
            conditionToEndMove = transform.localScale == TargetScale && transform.rotation == TargetRotation;
        }

        public void StartMovementRotationAndScale(Vector3 desiredPos, Vector3 desiredScale, Quaternion desiredRot)
        {
            InitialPosition = transform.position;
            InitialRotation = transform.rotation;
            InitialScale = transform.localScale;

            TargetPosition = desiredPos;
            TargetRotation = desiredRot;
            TargetScale = desiredScale;

            moveEnlapsedTime = 0f;
            doMoveTween = true;
            conditionToEndMove = transform.position == TargetPosition && transform.localScale == TargetScale && transform.rotation == TargetRotation;
        }
    }
}