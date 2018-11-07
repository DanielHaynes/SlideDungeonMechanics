﻿using System;
using UnityEngine;

public class KeyboardDetector : MonoBehaviour {

    public static event Action<KeyboardDirection> OnInput = delegate { };
    public KeyboardDirection direction;

    public void Update() {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) {
            direction = KeyboardDirection.Up;
            SendDirection();
        }else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
            direction = KeyboardDirection.Right;
            SendDirection();
        } else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
            direction = KeyboardDirection.Down;
            SendDirection();
        } else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
            direction = KeyboardDirection.Left;
            SendDirection();
        }

        //if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
        //    direction = KeyboardDirection.Up;
        //    SendDirection();
        //} else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
        //    direction = KeyboardDirection.Right;
        //    SendDirection();
        //} else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
        //    direction = KeyboardDirection.Down;
        //    SendDirection();
        //} else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
        //    direction = KeyboardDirection.Left;
        //    SendDirection();
        //}
    }

    void SendDirection() {
        OnInput(direction);
    }
}

public enum KeyboardDirection
{
    Up,
    Down,
    Left,
    Right
}
