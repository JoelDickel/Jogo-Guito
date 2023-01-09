using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionCenaPrincipal : MonoBehaviour {

	private bool comecou;
	private bool acabou;
	private bool podeReiniciar;

	private float scrollSpeed;
	private float velocidadeObjeto = -130f;
	private float posicaoZInicialObjetos = 4.5f;
	private GameObject objetoX;
	private int score;
	private int high_score;

	public Material materialPISO;
	public GameObject nodeRootCena;
	public GameObject OBSTACULO;
	public GameObject CAVEIRA;
	public GameObject CARRINHO;
	public GameObject CRISTAIS;
	public GameObject LUMINARIA;
	public GameObject GUITO;
	public GUIText textoMensagem;
	public GUIText textoScore;
	public GUIText textohigh_score;
	public AudioSource somMusica;
	public AudioClip somFinal;
	public AudioClip somHit;
	public AudioClip somDor;
	public AudioClip somVoa;
	public AudioClip somScore;

	void Start () {
		Time.timeScale = 1f;
		scrollSpeed = -0.24f;
		InvokeRepeating ("criaOBSTACULO", 1, 2f);
		InvokeRepeating ("criaCaveiraLuminariaCristais", 1, 1.7f);
		somMusica.loop = true;
		somMusica.volume = 0.2f;
		somMusica.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.time * scrollSpeed;
		materialPISO.SetTextureOffset("_MainTex", new Vector2(offset,-5));

		if(Input.anyKeyDown)
		{	
			if (!acabou)
			{

			if (comecou)
			{		
			voaGUITO();

			}else
								{
				GUITO.GetComponent<Rigidbody>().useGravity = true;
				comecou = true;
				scrollSpeed = -0.24f;
					textoMensagem.text = "";
				voaGUITO();
					textoScore.text = score.ToString ();
					textohigh_score.text = PlayerPrefs.GetInt ("high_score").ToString ();
			}
							} else {
				if (podeReiniciar)
				{ 
					apagaTodosObjetos ();
	}
			}
		}}

	void criaOBSTACULO()
	{
		if (comecou)
		{
			var offSetOBSTACULO = Random.Range (-1.8f, 0.0f);
			GameObject novoObjeto = (GameObject)Instantiate (OBSTACULO);
			novoObjeto.transform.parent = nodeRootCena.transform;
			novoObjeto.transform.position = new Vector3 (0, offSetOBSTACULO, posicaoZInicialObjetos);
			novoObjeto.transform.rotation = Quaternion.Euler (0, 0, 0);
			novoObjeto.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 0, velocidadeObjeto), ForceMode.Force);
		}
	}
	void criaCaveiraLuminariaCristais ()
	{
		var sorteiaObjeto = Random.Range (1, 5);
	
		switch (sorteiaObjeto) {

		case 1:
			objetoX = (GameObject)Instantiate (CAVEIRA);
			objetoX.transform.parent = nodeRootCena.transform;
					objetoX.transform.rotation = Quaternion.Euler (-90, 0, 0);
			break;

		case 2:
			objetoX = (GameObject)Instantiate (CARRINHO);
			objetoX.transform.parent = nodeRootCena.transform;
						objetoX.transform.rotation = Quaternion.Euler (-90, 0, 0);
			break;

		case 3:
			objetoX = (GameObject)Instantiate (CRISTAIS);
			objetoX.transform.parent = nodeRootCena.transform;
						objetoX.transform.rotation = Quaternion.Euler (-90, 0, 0);
			break;

		case 4:
			objetoX = (GameObject)Instantiate (LUMINARIA);
			objetoX.transform.parent = nodeRootCena.transform;
						objetoX.transform.rotation = Quaternion.Euler (-90, 0, 0);
			break;
		default: break;
		}
		objetoX.GetComponent<Rigidbody>().AddForce (new Vector3(0,0,velocidadeObjeto), ForceMode.Force);
}


	private void voaGUITO()
	{

		GUITO.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
		GUITO.GetComponent<Rigidbody>().AddForce (new Vector3 (0, 200,0), ForceMode.Force);
		GetComponent<AudioSource>().PlayOneShot(somVoa, 1f);
	}

	public void fimDeJogo()
	{
		if (!acabou) {
			acabou = true;
			print ("GameOver");
			comecou = false;
			scrollSpeed = -0.24f;
			Invoke ("setEstadoReload", 2);
			GetComponent<AudioSource> ().PlayOneShot (somHit, 0.4f);
			GetComponent<AudioSource> ().PlayOneShot (somDor, 0.7f);	
			if (score > PlayerPrefs.GetInt ("high_score"))
				PlayerPrefs.SetInt ("high_score", score);
		}
	}
		void setEstadoReload()
	{
		podeReiniciar = true;
		textoMensagem.text = "Fim de jogo. Toque para reiniciar!";
		GetComponent<AudioSource>().PlayOneShot(somFinal, 1f);
}
			void apagaTodosObjetos()
			{
				var gameObjects = GameObject.FindGameObjectsWithTag ("OBJETOS");
				for (var i = 0 ; i < gameObjects.Length ; i ++)
				{
					Destroy(gameObjects [i]);
				}
				GUITO.transform.position = new Vector3(0f,1f, -0.8f);
				GUITO.GetComponent<Rigidbody>().useGravity = true;
		comecou = true;
		acabou = false;
		podeReiniciar = false;
		scrollSpeed = -0.2f;
		voaGUITO();
		textoMensagem.text = "";
		score=0;
		textoScore.text = score.ToString ();
		}

	public void updateScore()	{
		score++;
		textoScore.text = score.ToString ();
		GetComponent<AudioSource>().PlayOneShot(somScore, 0.4f);
		if (score > PlayerPrefs.GetInt ("high_score"))
			high_score++;
					}
	}






			
