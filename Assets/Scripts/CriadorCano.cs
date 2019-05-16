using UnityEngine;

public class CriadorCano : MonoBehaviour
{
    [SerializeField] private GameObject cano;

    [SerializeField] private float fireRate = 0.7f;

    private float intervalo;

    void Update()
    {
        if (Time.time > intervalo)
        {
            Instantiate(cano, new Vector3(this.transform.position.x, Random.Range(-9.8f, 9.8f), 0), Quaternion.identity);

            intervalo = fireRate + Time.time;
        }
    }
}
