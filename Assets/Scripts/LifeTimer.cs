using UnityEngine;

public class LifeTimer : MonoBehaviour
{
    private float _delay = 2f;

    private void Start()
    {
        Destroy(gameObject, _delay);
    }
}
