using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptEventoVideo : DefaultTrackableEventHandler
{
	public Animator animator1;
	public GameObject textoInf;
	
	void Start()
    {
		base.Start();
		//animator1.Play("CanvasPpalAnimation", -1, 0f);
		animator1.Play("PantallaVideoInicio", -1, 0f);
		textoInf.SetActive(false);	
    }
	
	public void ShowPlayer(){
		//base.OnTrackingFound();		
		//animator1.Play("CanvasPpalAnimation", -1, 0f);
		animator1.Play("PantallaVideoVisible", -1, 0f);
		print("Reproduciendo Video");	
		textoInf.SetActive(false);		
	}
	
	public void HidePlayer(){
		animator1.Play("PantallaVideoOculto", -1, 0f);
		print("Ocultando Pantalla Video");	
		textoInf.SetActive(false);		
	}

	
	/*protected override void OnTrackingFound(){
		base.OnTrackingFound();		
		animator1.Play("CanvasPpalAnimation", -1, 0f);
		print("Reproduciendo Video");	
		textoInf.SetActive(false);		
	}
	protected override void OnTrackingLost(){
		base.OnTrackingLost();
		print("Video en Pausa");		
	}	*/	
	
	
	
	
	
	
   /* void Start()
    {
		base.Start();
		StartCoroutine(PrepareVideo());
        
    }
	
	private IEnumerator PrepareVideo(){
		videoPlayer.Prepare();
		while(!videoPlayer.isPrepared){
			yield return new WaitForSeconds(.5f);			
		}
		rawImage.texture = videoPlayer.texture;
		isVideoEnabled = true;
	}
	protected override void OnTrackingFound(){
		base.OnTrackingFound();
		if(isVideoEnabled) videoPlayer.Play();
		print("Reproduciendo Video");		
	}
	protected override void OnTrackingLost(){
		base.OnTrackingLost();
		if(isVideoEnabled) videoPlayer.Pause();
		print("Video en Pausa");		
	}	*/
}
