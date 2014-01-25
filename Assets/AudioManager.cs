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
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	//date is called once per frame
	void Update () {
	
	}

	public void playClip(string clip_name) {

		AudioClip clip = null;

		foreach (var c in Clips){
			if (c.name == clip_name){
				clip = c;
			}
		}
		if (clip != null) {

			var mc = GameObject.FindGameObjectWithTag ("MainCamera");
			var source = mc.AddComponent<AudioSource>();

			source.clip = clip;
			source.volume = Globals.Instance.SFX_VOLUME;
			source.PlayOneShot(clip);
		}
	}

}
