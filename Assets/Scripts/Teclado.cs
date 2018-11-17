using System.Collections.Generic;
using UnityEngine;

public class Teclado : MonoBehaviour {

    public Camera camara;
    public Transform t;
    public AudioClip[] notas;
    
    private Dictionary<string, Tecla> teclas;

    void Start () {
        teclas = new Dictionary<string, Tecla>();

        // Se obtiene el transform que contiene todas las teclas
       //  t = transform.Find("Whites");

        for (int i = 0; i < 2; i++)
        {
            Tecla tecla = t.GetChild(i).GetComponent<Tecla>();
            tecla.SetNota(notas[i]);
            teclas.Add(tecla.gameObject.name, tecla);
        }
    }

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(camara.ScreenPointToRay(Input.mousePosition), out hit))
            {
                teclas[hit.transform.parent.name].Reproducir();
            }
        }
    }
    
}
