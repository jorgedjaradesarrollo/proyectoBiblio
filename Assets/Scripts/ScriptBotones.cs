using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ScriptBotones : MonoBehaviour
{
	public bool isVideoEnabled;
	public VideoPlayer videoPlayer;
	public RawImage rawImage;
	public Animator animator1;
	
	public GameObject textoInf;
	
	void Awake(){
		videoPlayer.playOnAwake = false;
	}
    // Start is called before the first frame update
	void Start()
    {			
		StartCoroutine(PrepareVideo()); 				
    }
		
	public void dalePlay(){		
		videoPlayer.GetComponent<AudioSource> ().volume=1;	
		if(isVideoEnabled) videoPlayer.Play(); 		
	}
	public void dalePausa(){
		if(isVideoEnabled) videoPlayer.Pause();
		print("Video en Pausa"); 		
	}	
	public void daleStop(){
		
		if(isVideoEnabled) videoPlayer.Stop();
		print("Video en Stop"); 		
		StartCoroutine(PrepareVideo());
	}
	private IEnumerator PrepareVideo(){
		videoPlayer.Prepare();
		videoPlayer.GetComponent<AudioSource> ().volume=0;	
		while(!videoPlayer.isPrepared){
			yield return new WaitForSeconds(.2f);			
		}
		rawImage.texture = videoPlayer.texture;
		isVideoEnabled = true;
	}
	public void OcultarCanvas(){
		textoInf.SetActive(false);
		if(isVideoEnabled) videoPlayer.Pause();
		animator1.Play("CanvasPpalSalirAnimation", -1, 0f);			
		textoInf.SetActive(true);		
	}
	
}
