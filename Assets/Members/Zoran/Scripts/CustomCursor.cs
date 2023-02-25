using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D mousePointer;
    private Vector2 hotspot;

    void Start()
    {
        Cursor.SetCursor(mousePointer, hotspot, CursorMode.Auto);
    }
}
