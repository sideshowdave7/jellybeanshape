using UnityEngine;
using System.Collections;

public class ColorBehavior : MonoBehaviour {

	public GameObject glowParticles;
	private Material shapeMaterials;

	private bool _lerpMaterial = false;
	private float _lerpTimer = 0f;

	private float _lerpTime = 1f;

	private Color _colorFrom;
	private Color _colorTo;

	public ShapeType shapeType;

	private Color STAR = new Color (1f,0.22f,0.27f);
	private Color PENTAGON = new Color (1f,0.04f,0.54f);
	private Color SQUARE = new Color (0.81f,0.04f,1f);
	private Color TRIANGLE = new Color (0.35f,0.04f,1f);
	private Color CIRCLE = new Color (0,0.73f,0.54f);

	void Start () 
	{
		glowParticles = (GameObject)Instantiate(glowParticles);
		glowParticles.transform.position = new Vector3 (this.transform.position.x,this.transform.position.y, this.transform.position.z+0.5f );
		glowParticles.transform.parent = this.transform;
	}

	void Update ()
	{
		if ( _lerpMaterial ) { Lerp_Color(); }
	}

	public void Set_Color (ShapeType shape)  
	{
		shapeType = shape;
		if (shape == ShapeType.Circle ) 	{ _colorFrom = renderer.material.color; _colorTo = CIRCLE;  }
		if (shape == ShapeType.Square ) 	{ _colorFrom = renderer.material.color; _colorTo = SQUARE; }
		if (shape == ShapeType.Triangle ) 	{ _colorFrom = renderer.material.color; _colorTo = TRIANGLE;  }
		if (shape == ShapeType.Hexagon ) 	{ _colorFrom = renderer.material.color; _colorTo = PENTAGON;  }
		if (shape == ShapeType.Star )       { _colorFrom = renderer.material.color; _colorTo = STAR; }

		_lerpTimer = 0f;
		_lerpMaterial = true;

	}

	void Lerp_Color ()
	{
		_lerpTimer += Time.deltaTime / _lerpTime;
		 
		this.renderer.material.color = Color.Lerp(_colorFrom,_colorTo,_lerpTimer);
		glowParticles.particleSystem.startColor = Color.Lerp(_colorFrom,_colorTo,_lerpTimer);
	

		if (_lerpTimer >= 1f)
		{
			if (!glowParticles.particleSystem.isPlaying && this.gameObject.GetComponent<ShapeBehavior>()._target.tag == "Player")
				glowParticles.particleSystem.Play();
			_lerpMaterial = false;
			_lerpTimer = 0f;
		}
	}
}
