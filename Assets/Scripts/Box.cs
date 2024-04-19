using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MouseButtonChecker))]
[RequireComponent(typeof(Renderer))]

public class Box : MonoBehaviour
{
    [SerializeField] private Box _boxPrefab;
    [SerializeField] private float _currentSeparateChance;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private MouseButtonChecker _button;
    private Material _material;
    private float _factor = 0.5f;

    private void Awake()
    {
        _button = GetComponent<MouseButtonChecker>();
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
        float scaleFactor = 1 / transform.localScale.magnitude;

        Instantiate(_explosion, transform.position, Quaternion.identity).transform.localScale *= scaleFactor;

        foreach (Rigidbody box in GetExplodableObjects())
        {
            box.AddExplosionForce(_explosionForce * scaleFactor, transform.position, _explosionRadius * scaleFactor);
        }

        Destroy(gameObject);
    }

    private List<Rigidbody> GetExplodableObjects()
    {        
        return Physics.OverlapSphere(transform.position, _explosionRadius).Where(hit => hit.attachedRigidbody != null).Select(hit => hit.attachedRigidbody).ToList();
    }
}
