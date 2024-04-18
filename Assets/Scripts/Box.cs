using UnityEngine;

[RequireComponent(typeof(PressMouseButton))]

public class Box : MonoBehaviour
{
    [SerializeField] private Box _boxPrefab;
    [SerializeField] private float _currentSeparateChance;
    [SerializeField] private ParticleSystem _explosion;

    private PressMouseButton _button;
    private Material _material;
    private float _factor = 0.5f;

    private void Awake()
    {
        _button = GetComponent<PressMouseButton>();
        _material = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        _button.IsPressed += ChooseBehaviour;
    }

    private void OnDisable()
    {
        _button.IsPressed -= ChooseBehaviour;
    }

    public void SetNewLifeProperties()
    {
        ChangeScale();
        ChangeColor();
        ChangeSeparateChance();
    }

    private void ChangeScale()
    {
        transform.localScale = transform.localScale * _factor;
    }

    private void ChangeColor()
    {
        _material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void ChangeSeparateChance()
    {
        _currentSeparateChance *= _factor;
    }

    private void ChooseBehaviour()
    {
        int maxChance = 100;

        if (Random.Range(0, maxChance) < _currentSeparateChance)
        {
            Separate();
        }
        else
        {
            Explode();
        }
    }

    private void Separate()
    {
        int minCount = 2;
        int maxCount = 7;

        int count = Random.Range(minCount, maxCount);

        for (int i = 0; i < count; i++)
        {
            Box createdBox = Instantiate(_boxPrefab, transform.position, Random.rotation);

            createdBox.SetNewLifeProperties();
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity).transform.localScale *= transform.localScale.magnitude;

        Destroy(gameObject);
    }
}
