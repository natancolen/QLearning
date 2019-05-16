using UnityEngine;

public class Cano : MonoBehaviour
{
    [Range(3, 27)]
    public float velocidade = 9.0f;

    void Update()
    {

        this.transform.Translate(-velocidade * Time.deltaTime, 0, 0);

        if( this.transform.position.x < 7.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
