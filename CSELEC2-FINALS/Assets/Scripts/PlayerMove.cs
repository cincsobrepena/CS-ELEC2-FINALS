using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    public float speed = 20f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(0, verticalInput, 0);  
        transform.Translate( movement * speed * Time.deltaTime);
    }
}
