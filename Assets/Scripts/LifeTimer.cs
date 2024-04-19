using UnityEngine;

public class LifeTimer : MonoBehaviour
{
    private float _delay = 2f;

    private void Update()
    {
        Destroy(gameObject, _delay);
    }
}
