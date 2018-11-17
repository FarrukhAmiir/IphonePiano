using UnityEngine;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (AudioSource))]
public class Tecla : MonoBehaviour {
    
    private Animator anim;
    private AudioSource audio;
    
	void Start () {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
	}

    public void Reproducir () {
        // TODO: Reproducir sonido
        anim.SetTrigger("Activar");
        audio.Play();
    }

    public void SetNota (AudioClip nota) {
        audio.clip = nota;
    }
}
