using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdministradorApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
        /// <summary> Muestra las opciones de la app </summary>            
        public void CargarInicio()
        {
            StartCoroutine("cargarEscena", "Inicio");
        }	
        /// <summary> Carga la escena de Escaneo de Libros </summary>            
        public void CargarEscaneoLibros()
        {
            StartCoroutine("cargarEscena", "EscaneoLibros");
        }			
        /// <summary> Carga la escena de Escaneo de Estantes </summary>            
        public void CargarEscaneoEstantes()
        {
            StartCoroutine("cargarEscena", "EscaneoEstantes");
        }					
        /// <summary>Carga escena segun corresponda</summary>
        IEnumerator cargarEscena(string escena_a_cargar)
        {
            yield return new WaitForSeconds(0.8f);
            SceneManager.LoadScene(escena_a_cargar);
        }
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }	
}
