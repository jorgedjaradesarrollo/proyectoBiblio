using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace MyYouTubeVideoPlayer{
	
public class YouTubeVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    
    [InspectorName("YouTube URL")]
    public string youTubeUrl;
	
	public GameObject texto_cargando;
    
	private bool isInitial;
	private bool isReset;
	private string url_yout_ant;
	private string url_yout;

	
    private void Awake()
    {
		texto_cargando.SetActive(false);
		isInitial=false;
		isReset=false;
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();			
        }
    }

    /*private void Start()
    {
        StartCoroutine(SetVideoPlayerUrl());
    }*/
	
	private void Update(){
			
			url_yout=PlayerPrefs.GetString ("URL_YOUTUBE",""); 
			if(youTubeUrl != url_yout){
				youTubeUrl=url_yout;
				print("LA URL EN YOUTUBE (YouTubeVideoPlayer) UPDATE:........."+url_yout);
			}
			
	}
    
    private IEnumerator SetVideoPlayerUrl()
    {
		texto_cargando.SetActive(true);
        if (videoPlayer == null)
        {
            Debug.LogError("No video player.");
			print("No video player.");
            yield break;
        }else{print("TIENE video player.");}
		
        
		var request = new YouTubeRequest(youTubeUrl);
        yield return request.SendRequest();

        if (request.Result == YouTubeRequestResult.Error)
        {
            Debug.LogError($"Failed to fetch YouTube video details: {request.Error}");
            yield break;
        }
        
        Debug.Log("Fetched YouTube formats.");
		print("CARGANDO VIDEO ......");
        try
        {
            videoPlayer.url = request.BestQualityFormat.url;
			print("CARGANDO VIDEO ...");
			
			videoPlayer.Play();//LO AGREGUE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			StartCoroutine("MostrarDesactivado");//LO AGREGUE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        }
        catch (InvalidOperationException)
        {
            Debug.LogError("Failed to find any compatible formats.");
        }
    }

	public void dalePlay(){	
		if(isReset){
			isReset=false;
			videoPlayer.frame = 0;//Vuelve al inicio del video
			videoPlayer.Play();
		}else{	
			if(!isInitial){	
				print("PAUSA ESTA EN FALSO");	
				isInitial=true;
				StartCoroutine(SetVideoPlayerUrl());	
			}else{
				print("PAUSA ESTA EN VERDADERO");	
				videoPlayer.Play();
			}
		}
		
	}
	public void dalePausa(){
		videoPlayer.Pause();		
		print("Video en Pausa"); 		
	}	
	public void daleStop(){
		isReset=true;
		videoPlayer.Pause();
		print("Video en Stop"); 		
		//StartCoroutine(PrepareVideo());
	}
		
        IEnumerator MostrarDesactivado()
        {
            yield return new WaitForSeconds(10);
            texto_cargando.SetActive(false);
        }
        /// <summary></summary>
        IEnumerator MostrarActivado()
        {
            yield return new WaitForSeconds(0.05f);
            texto_cargando.SetActive(true);
        }	
	
}

}