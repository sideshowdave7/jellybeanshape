using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	private static AudioManager _instance = null;


	public List<AudioClip> Clips;

	public static AudioManager Instance {
		get { return _instance; }
	}
	
	void Awake ()
	{

		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			_instance = this;
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	//date is called once per frame
	void Update () {
	
	}
	public void playLoop(string clip_name) {

		bool isPlaying = false;
		foreach (var source in GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>()){
			if (source.clip.name == clip_name && source.isPlaying == true) {
				isPlaying = true;
			}
		}
		
		if (!isPlaying) {

			AudioClip clip = null;
			
			foreach (var c in Clips){
				if (c.name == clip_name){
					clip = c;
				}
			}
			if (clip != null) {
				
				var mc = GameObject.FindGameObjectWithTag ("MainCamera");
				var source = mc.AddComponent<AudioSource>();
				source.loop = true;
				source.clip = clip;
				source.volume = Globals.Instance.SFX_VOLUME;
				source.Play();
			}
		}
	}
	public void playClip(string clip_name) {


		AudioClip clip = null;
		bool isPlaying = false;
		AudioSource _as = null;
		foreach (var source in GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>()){
			if (source.clip.name == clip_name && source.isPlaying == true) {
				isPlaying = true;
			}
		}

		foreach (var source in GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>()){
			if (source.clip.name == clip_name) {
				_as = source;
			}
		}

		if (!isPlaying) {

			foreach (var c in Clips){
				if (c.name == clip_name){
					clip = c;
				}
			}
			if (clip != null) {

				var mc = GameObject.FindGameObjectWithTag ("MainCamera");

				if (_as == null) {
					var source = mc.AddComponent<AudioSource>();
					source.clip = clip;
					source.volume = Globals.Instance.SFX_VOLUME;
					source.Play();
				} else {
					_as.Play();
				}
			}

		}
	}

}
