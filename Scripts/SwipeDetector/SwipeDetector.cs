﻿using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private Vector2 fingerMovePosition;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 200f;

    public static event Action<SwipeData> OnSwipe = delegate { };

    private void Update() {
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
                fingerMovePosition = touch.position;
                forcastMet = false;
            }

            if (touch.phase == TouchPhase.Moved) {
                fingerMovePosition = touch.position;

                if (!detectSwipeOnlyAfterRelease) {
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                }
            }

            if (touch.phase == TouchPhase.Ended) {
                fingerDownPosition = touch.position;
                DetectSwipe();
                forcastMet = true;
            }
        }
    }

    private void DetectSwipe() {
        if (SwipeDistanceCheckMet()) {
            if (IsVerticalSwipe()) {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            } else {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool IsVerticalSwipe() {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool IsVerticalForcast() {
        return VerticalMovementDistanceForcast() > HorizontalMovementDistanceForcast();
    }

    private bool SwipeDistanceCheckMet() {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistanceForcast() {
        return Mathf.Abs(fingerDownPosition.y - fingerMovePosition.y);
    }

    private float HorizontalMovementDistanceForcast() {
        return Mathf.Abs(fingerDownPosition.x - fingerMovePosition.x);
    }

    private float VerticalMovementDistance() {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance() {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction) {
        SwipeData swipeData = new SwipeData() {
            Direction = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };
        OnSwipe(swipeData);
    }
}

public struct SwipeData {
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection {
    Up,
    Down,
    Left,
    Right
}
