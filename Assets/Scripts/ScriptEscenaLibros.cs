using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEscenaLibros : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString ("ESCENA_TARGET","LIBRO"); //ES TARGET DE LIBRO O TARGET ESTANTE SEGUN EL PREFIJO DEL NOMBRE DEL TARGET
    }

}
