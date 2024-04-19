using System;
using UnityEngine;

public class MouseButtonChecker : MonoBehaviour
{
    public event Action IsPressed;

    private void OnMouseUpAsButton()
    {
        IsPressed?.Invoke();
    }
}
