using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D sprite;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = new Vector2(0.2f,-1.95f);
    public int size;
    private void Start()
    {
        Cursor.SetCursor(ResizeTexture(sprite, size, size), hotSpot, cursorMode);
    }
    Texture2D ResizeTexture(Texture2D srcTexture, int newWidth, int newHeight)
    {
        var resizedTexture = new Texture2D(newWidth, newHeight, TextureFormat.RGBA32, false);
        Graphics.ConvertTexture(srcTexture, resizedTexture);
        return resizedTexture;
    }

    private void OnDisable()
    {
        Cursor.SetCursor(sprite, Vector2.zero, cursorMode);
    }
}
