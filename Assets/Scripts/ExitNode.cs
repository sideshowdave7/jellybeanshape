using UnityEngine;
using System.Collections;

public class ExitNode : MonoBehaviour {

	private SlotBehavior[] levelSlots;

	public GameObject transitionSlide;
	public GameObject levelGUIText;
	public string levelText;
	public int textSize;

	private bool _introTransition = true;
	private bool _endTransition = false;
	private bool _levelComplete = false;
	private bool _displayText = false;
	private float _levelEndCounter = 0;
	private Color _transitionSlideColor = new Color (0.507f, 0.679f, 0.781f, 1f);

	void Start () 
	{
		//find all the slots in the level
		GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");

		levelSlots = new SlotBehavior[slots.Length];

		for (int i = 0; i < slots.Length ; i ++ )
		{
			levelSlots[i] = slots[i].GetComponent<SlotBehavior>();
		}

		transitionSlide = (GameObject)Instantiate(transitionSlide);
		transitionSlide.transform.position = new Vector3 (0,0,-5f);

		levelGUIText = (GameObject)Instantiate(levelGUIText);
		levelGUIText.guiText.fontSize = textSize;
		levelGUIText.guiText.text = "";


		
	}
	
	void Update () 
	{
		if (_levelComplete == false)
		{
			bool checkLocks = true;

			//check if the exit condidtion has been satisfied
			for (int i = 0; i < levelSlots.Length ; i ++ )
			{
				if (levelSlots[i].locked == false) {checkLocks = false;}
			}
			
			if ( checkLocks )
			{ 
				_levelComplete = true; 
				StartCoroutine(TweenDoorLightForGreatJustice());
				Globals.Instance.LEVEL_COMPLETE = true;
				AudioManager.Instance.playLoop("MainDrumLoop");

			}
		}

		else
		{
			if ( _endTransition )
			{
				End_Transition();
			}
		}

		if (_displayText)
		{
			Display_Text();
		}

		if (_introTransition)
		{
			Intro_Transition ();
		}
	}

	/// <summary>
	/// Drinks A hoagie.
	/// </summary>
	void DrinkAHoagie()
	{
		Debug.Log("FOR GREAT HOAGIE");
	}


	/// <summary>
	/// Tweens the door light for great justice.
	/// </summary>
	/// <returns>The door light for great justice.</returns>
	IEnumerator TweenDoorLightForGreatJustice()
	{
		float duration = 1f;
		float elapsed = 0f;
		float x;
		while (elapsed < duration){

			elapsed += Time.deltaTime;
			x = elapsed*1.5f;
			this.transform.FindChild("TEHLIGHT").localScale = new Vector2(x, 1.5f);
			yield return null;
		}
		yield return null;
	}

	void Intro_Transition ()
	{
		if (transitionSlide.renderer.material.color.a > 0)
		{
			_transitionSlideColor.a -= 0.02f;
			transitionSlide.renderer.material.color = _transitionSlideColor;
		}
		else
		{
			_introTransition = false;
		}
	}

	void End_Transition()
	{
		if (transitionSlide.renderer.material.color.a < 1)
		{
			_transitionSlideColor.a += 0.02f;
			transitionSlide.renderer.material.color = _transitionSlideColor;
		}
		else
		{
			_displayText = true;
			_endTransition = false;
		}

	}

	void Display_Text ()
	{

		_levelEndCounter += Time.deltaTime;

		if ( Mathf.Floor( _levelEndCounter ) == 1 )
		{
			if (_levelEndCounter - 1 < 1) levelGUIText.guiText.color = new Color(1,1,1,_levelEndCounter - 1);
			levelGUIText.guiText.text = levelText;
		}

		if ( _levelEndCounter > 10 )
		{
			if ( (_levelEndCounter - 10) < 1 )
			{
				levelGUIText.guiText.color = new Color(1,1,1,(_levelEndCounter - 11)*-1);
			}

			if (_levelEndCounter > 12)
			{
				int level = Application.loadedLevel;
				Globals.Instance.LEVEL_COMPLETE = false;
				Application.LoadLevel(level + 1);
			}

		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.name == "MainCharacter" && _levelComplete )
		{
			_endTransition = true;
		}
	}
}
