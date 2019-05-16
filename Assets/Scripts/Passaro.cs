using UnityEngine;
using UnityEngine.SceneManagement;

public class Passaro : MonoBehaviour
{
    [SerializeField] private float aceleracao = 5f;
    [SerializeField] private float fireRate = 1f;

    private Q_Learning matrizAprendizado;

    private Rigidbody rb;

    private int num_Estados = 3;
    private int num_Acoes = 2;

    private float[,] recompensa = new float[3,2];
    private float[,] punicao = new float[3, 2];
    private float taxa = 0.1f;
    private float intervalo;

    void Start()
    {
        recompensa[0, 0] = recompensa[0, 1] = 0.5f;
        recompensa[1, 0] = recompensa[2, 1] = 1;
       
        punicao[0, 0] = punicao[0, 1] = -0.5f; 
        punicao[1, 1] = punicao[2, 0] = -1; 

        matrizAprendizado = new Q_Learning( num_Estados, num_Acoes, recompensa, punicao, taxa);
        matrizAprendizado.Treinamento(10);

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        int aux = 1;
        int layerMask = 1;

        if (Time.time > intervalo)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RaycastHit hit, Mathf.Infinity, layerMask))
            {

                if (hit.collider.tag.Equals("Alinhado"))
                {
                    aux = matrizAprendizado.Executar(0);
                }
                else if (hit.collider.tag.Equals("Abaixo"))
                {
                    aux = matrizAprendizado.Executar(1);
                }
                else if (hit.collider.tag.Equals("Acima"))
                {
                    aux = matrizAprendizado.Executar(2);
                }
            }

            if (aux == 0)
            {
                rb.AddForce(Vector3.up * aceleracao, ForceMode.Impulse);
            }
            intervalo = fireRate + Time.time;
        }

        if  (transform.position.y < -8.8f || transform.position.y > 35.2f)
        {
            SceneManager.LoadScene(0);
        }
    }
}
