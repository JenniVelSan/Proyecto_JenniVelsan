using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject menuGameOver;
    public float velocidad = 2;
    public GameObject col;
    public GameObject obstaculo1;
    public GameObject obstaculo2;
    public Renderer fondo;
    public bool gameOver = false;
    public bool start = false;
    public float alturaCol = -4; // valor predeterminado para la posición vertical
    public float alturaObstaculos = -3; // valor predeterminado para los obstáculos

    public List<GameObject> cols;
    public List<GameObject> obstaculos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Crear Mapa
        for (int i = 0; i< 21; i++)
        {
           cols.Add( Instantiate(col, new Vector2(-10 + i, -6), Quaternion.identity));
        }
        //Crear Piedras
        obstaculos.Add(Instantiate(obstaculo1, new Vector2(14, alturaObstaculos), Quaternion.identity));
        obstaculos.Add(Instantiate(obstaculo2, new Vector2(18, alturaObstaculos), Quaternion.identity));
    }
    IEnumerator GenerarObstaculos()
    {
        StartCoroutine(GenerarObstaculos());

        while (!gameOver)
        {
            // Espera entre 1 y 3 segundos (más aleatorio)
            float espera = Random.Range(1f, 3f);
            yield return new WaitForSeconds(espera);

            // Elige aleatoriamente si usar obstaculo1 o obstaculo2
            GameObject prefab = (Random.value > 0.5f) ? obstaculo1 : obstaculo2;

            // Instanciar en la parte derecha
            GameObject nuevoObs = Instantiate(prefab, new Vector2(14, alturaObstaculos), Quaternion.identity);
            nuevoObs.tag = "Obstaculo";
            obstaculos.Add(nuevoObs);
        }
    }
    // Update is called once per frame
    void Update()
    {
            if (start == false)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    start = true;
            }
        }

        if (start == true && gameOver == true)
        {
            menuGameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (start == true && gameOver == false)

        {
            menuPrincipal.SetActive(false);
            fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.015f, 0) * Time.deltaTime;

        //Mover Mapa
        for (int i = 0; i < cols.Count; i++)
        {
            if (cols[i].transform.position.x <= -10)
            {
                cols[i].transform.position = new Vector3(10, -6, 0);
            }

                cols[i].transform.position = cols[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }
            //Mover obstaculos
            for (int i = 0; i < obstaculos.Count; i++)
            {
                if (obstaculos[i].transform.position.x <= -10)
                {
                    float randomObs = Random.Range(11, 18);
                    obstaculos[i].transform.position = new Vector3(randomObs, alturaObstaculos, 0);

                }

                obstaculos[i].transform.position = obstaculos[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }
        }
    }

}