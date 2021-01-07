using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeMouse : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursor;

    void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
