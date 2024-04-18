using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    private float _delay = 2f;

    private void Update()
    {
        Destroy(gameObject, _delay);
    }
}
