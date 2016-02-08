using UnityEngine;
using System.Collections;

// Alternativa para animaçao de sprites.

[RequireComponent(typeof(SpriteRenderer))]
public class Animacao : MonoBehaviour {
	public bool loop = true;
	public float fps = 12f;
	public Sprite[] sprites;
	public AudioSource efeito = null;

	private SpriteRenderer render;
	private float tStart = 0f;
	private float tPause = 0f;

	void Awake () {
		play();
		// pega o componente SpriteRenderer
		render = GetComponent<SpriteRenderer>();
	}

	// toca a animacao a partir do primeiro quadro
	public void play () {
		tStart = Time.time;
		tPause = 0f;
		if (efeito) {
			efeito.Play();
		}
	}

	void Update () {
		if (tStart==0f)
			tStart = Time.time;
		if (tStart!=-2f)
			render.sprite = getSprite();
	}

	//- retorna o sprite selecionado para o time atual.
	public Sprite getSprite () {
		return sprites[ calcPos() ];
	}

	//- retorna a posiçao selecionada para o time atual.
	public int calcPos () {
		int pos = calcPos2();
		if (loop) {
			return pos % sprites.Length;
		} else {
			if (pos >= sprites.Length)
				return sprites.Length-1;
			return pos;
		}
	}

	private int calcPos2 () {
		float tempo = Time.time - tStart;
		if (tPause>0f)
			tempo = tPause - tStart;
		return (int) (tempo*fps);
	}

	//- calcula o numero de voltas dadas pelo sprite.
	public float calcLoops () {
		return calcPos2() / sprites.Length;
	}

	// retoma o tempo do sprite se o script for reativado
	void OnEnable () {
		if (tPause!=0f) {
			tStart += Time.time - tPause;
			tPause = 0f;
		}
	}

	// pausa o sprite se o script for desativado
	void OnDisable () {
		if (tPause==0f)
			tPause = Time.time;
	}

}
