using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    public Vector2 MoveInput { get; private set; }
    
    public Vector3 MousePosition { get; private set; }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        MoveInput = new Vector2(h, v);
        MousePosition = Input.mousePosition;
    }
}
