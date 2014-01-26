using UnityEngine;
using System.Collections;

public class ExitNode : MonoBehaviour {

	private SlotBehavior[] levelSlots;

	public GameObject transitionSlide;

	private bool _endTransition = false;
	private bool _levelComplete = false;
	private bool _displayText = false;

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
		transitionSlide.transform.position = new Vector3 (0,-50f,-5f);


	}
	
	// Update is called once per frame
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
			
			if ( checkLocks ){ _levelComplete = true; }
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
	}

	void End_Transition()
	{


		if (transitionSlide.transform.position.y < 0)
		{
			transitionSlide.transform.Translate(0,0.75f,0);
		}
		else
		{
			_displayText = true;
			_endTransition = false;
		}

	}

	void Display_Text ()
	{

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.name == "MainCharacter")
		{
			_endTransition = true;
		}
	}
}
