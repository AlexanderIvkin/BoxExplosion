using System;
using UnityEngine;

public class PressMouseButton : MonoBehaviour
{
    public event Action IsPressed;

    private void OnMouseUpAsButton()
    {
        IsPressed?.Invoke();
    }
}
