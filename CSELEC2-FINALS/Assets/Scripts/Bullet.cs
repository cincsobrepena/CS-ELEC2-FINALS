using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Renderer bulletRenderer;

    private void Awake()
    {
        bulletRenderer = GetComponent<Renderer>();
    }

    public void SetColor(Color color)
    {
        if (bulletRenderer != null)
        {
            bulletRenderer.material.color = color;
        }
    }
}

