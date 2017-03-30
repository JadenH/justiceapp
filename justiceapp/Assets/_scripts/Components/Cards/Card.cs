using System;
using DG.Tweening;
using TouchScript.Gestures;
using UnityEngine;

namespace _scripts
{
    public abstract class Card : DragonBehaviour
    {
        public ScreenTransformGesture ScreenTransformGesture;

        private Vector2 _swipeStartPosition;
        private Vector2 _swipeEndPosition;
        protected Vector3 StartingPosition;

        private int _swipeDistanceHorizontal = 100;
        private int _swipeDistanceVertical = 150;

        protected override void Awake()
        {
            //            _swipeDistanceVertical = Mathf.FloorToInt(Screen.height * .1f);
            //            _swipeDistanceHorizontal = Mathf.FloorToInt(Screen.width * .1f);

            StartingPosition = transform.position;
            base.Awake();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ScreenTransformGesture.TransformStarted += HandleTransformStarted;
            ScreenTransformGesture.Transformed += HandleTransform;
            ScreenTransformGesture.TransformCompleted += HandleTransformCompleted;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ScreenTransformGesture.TransformStarted -= HandleTransformStarted;
            ScreenTransformGesture.Transformed -= HandleTransform;
            ScreenTransformGesture.TransformCompleted -= HandleTransformCompleted;
        }

        protected virtual bool SwipeUp() { return true; }
        protected virtual bool SwipeDown() { return true; }
        protected virtual bool SwipeLeft() { return true; }
        protected virtual bool SwipeRight() { return true; }

        private void HandleTransformCompleted(object sender, EventArgs e)
        {
            var distance = _swipeStartPosition - _swipeEndPosition;
            if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
            {
                if (distance.x < -_swipeDistanceHorizontal)
                {
                    if (SwipeRight()) ReturnCardToStart();
                }
                else if (distance.x > _swipeDistanceHorizontal)
                {
                    if (SwipeLeft()) ReturnCardToStart();
                }
                else
                {
                    ReturnCardToStart();
                }
            }
            else
            {
                if (distance.y > _swipeDistanceVertical)
                {
                    if (SwipeUp()) ReturnCardToStart();
                }
                else if (distance.y < -_swipeDistanceVertical)
                {
                    if (SwipeDown()) ReturnCardToStart();
                }
                else
                {
                    ReturnCardToStart();
                }
            }
        }

        private void ReturnCardToStart()
        {
            transform.DOMove(StartingPosition, .5f);
        }

        private void HandleTransformStarted(object sender, EventArgs e)
        {
            Gesture gesture = (Gesture)sender;

            _swipeStartPosition = gesture.ScreenPosition;
        }

        private void HandleTransform(object sender, EventArgs eventArgs)
        {
            Gesture gesture = (Gesture)sender;
            _swipeEndPosition = gesture.ScreenPosition;

            Vector3 newPos = gesture.ScreenPosition - gesture.PreviousScreenPosition;
            transform.position += newPos;
        }
    }
}