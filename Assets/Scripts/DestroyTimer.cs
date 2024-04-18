using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
