using UnityEngine;
using System.Collections;

public class Globals 
{
	private Globals ()
	{
		globals_init ();
	}

	private void globals_init()
	{

	}

	private static Globals _instance = new Globals ();

	/// <summary>Singleton instance for the active Globals.</summary>
	/// <value>Singleton instance for the active Globals.</value>
	public static Globals Instance 
	{
		get {
			if (_instance == null) {
				_instance = new Globals ();  
			}
			return _instance;
		}
	}

	#region PLAYER_VARS

	public float PLAYER_SPEED = 150f;
	public float SLOT_TO_SHAPE_DISTANCE = 12f;
	public float SFX_VOLUME = 1f;
	public float INFLUENCE_RADIUS = 15f;
	public bool LEVEL_COMPLETE = false;

	#endregion


}

public enum ShapeType {
	
	Circle,
	Square,
	Triangle,
	Hexagon,
	Star
	
}