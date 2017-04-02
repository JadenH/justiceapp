﻿using System;
using DG.Tweening;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

namespace _scripts
{
    public abstract class Card : DragonBehaviour
    {
        public ScreenTransformGesture ScreenTransformGesture;

        protected bool DisableCardSwipe = false;

        private Vector2 _swipeStartPosition;
        private Vector2 _swipeEndPosition;
        protected Vector3 StartingPosition;

        protected int SwipeDistanceHorizontal = 100;
        protected int SwipeDistanceVertical = 100;

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

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
            if (DisableCardSwipe) return;
            var distance = _swipeStartPosition - _swipeEndPosition;
            if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
            {
                if (distance.x < -SwipeDistanceHorizontal)
                {
                    if (SwipeRight())
                    {
                        ReturnCardToStart();
                    }
                    else
                    {
                        SwipeCardAnimate(Direction.Right);
                    }
                }
                else if (distance.x > SwipeDistanceHorizontal)
                {
                    if (SwipeLeft())
                    {
                        ReturnCardToStart();
                    }
                    else
                    {
                        SwipeCardAnimate(Direction.Left);
                    }
                }
                else
                {
                    ReturnCardToStart();
                }
            }
            else
            {
                if (distance.y > SwipeDistanceVertical)
                {
                    if (SwipeUp())
                    {
                        ReturnCardToStart();
                    }
                    else
                    {
                        SwipeCardAnimate(Direction.Up);
                    }
                }
                else if (distance.y < -SwipeDistanceVertical)
                {
                    if (SwipeDown())
                    {
                        ReturnCardToStart();
                    }
                    else
                    {
                        SwipeCardAnimate(Direction.Down);
                    }
                }
                else
                {
                    ReturnCardToStart();
                }
            }
        }

        protected void SwipeCardAnimate(Direction direction)
        {
            DisableCardSwipe = true;
            switch (direction)
            {
                case Direction.Left:
                    transform.DOLocalMove(Vector2.Max((StartingPosition - transform.position) * -2, GetDirectionVector(direction) * Screen.width * 2), 1f);
                    break;
                case Direction.Right:
                    transform.DOLocalMove(Vector2.Min((StartingPosition - transform.position) * -2, GetDirectionVector(direction) * Screen.width * 2), 1f);
                    break;
                case Direction.Up:
                    transform.DOLocalMove(GetDirectionVector(direction) * Screen.height * -2, 1f);
                    break;
                case Direction.Down:
                    transform.DOLocalMove(GetDirectionVector(direction) * Screen.height * -2, 1f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }

        private Vector2 GetDirectionVector(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return Vector2.left;
                case Direction.Right:
                    return Vector2.right;
                case Direction.Up:
                    return Vector2.up;
                case Direction.Down:
                    return Vector2.down;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }

        private void ReturnCardToStart()
        {
            transform.DOMove(StartingPosition, .5f);
            ReturnCard();
        }

        protected virtual void ReturnCard() { }

        private void HandleTransformStarted(object sender, EventArgs e)
        {
            if (DisableCardSwipe) return;
            Gesture gesture = (Gesture)sender;

            _swipeStartPosition = gesture.ScreenPosition;
        }

        private void HandleTransform(object sender, EventArgs eventArgs)
        {
            if (DisableCardSwipe) return;
            Gesture gesture = (Gesture)sender;
            _swipeEndPosition = gesture.ScreenPosition;

            var newPos = transform.position + (Vector3)(gesture.ScreenPosition - gesture.PreviousScreenPosition);

            float maxdistance = Mathf.Min(Screen.width, Screen.height) * .3f;
            float dist = (-Mathf.Pow(Vector2.Distance(StartingPosition, transform.position), 2) / maxdistance + maxdistance) / maxdistance;

            transform.position = Vector2.Lerp(transform.position, newPos, Mathf.Max(dist, 0f));
        }

    }
}