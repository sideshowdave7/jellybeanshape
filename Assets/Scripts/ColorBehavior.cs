using UnityEngine;
using System.Collections;

public class ColorBehavior : MonoBehaviour {

	public GameObject glowParticles;
	private Material shapeMaterials;

	private bool _lerpMaterial = false;
	private float _lerpTimer = 0;

	private Color _colorFrom;
	private Color _colorTo;
	private Color _glowFrom;
	private Color _glowTo;

	private Color STAR = new Color (1f,0.22f,0.27f);
	private Color STAR_GLOW = new Color (1, 0.57f, 0.43f);
	private Color PENTAGON = new Color (1f,0.04f,0.54f);
	private Color PENTAGON_GLOW = new Color (1, 0.7f, 0.85f);
	private Color SQUARE = new Color (0.81f,0.04f,1f);
	private Color SQUARE_GLOW = new Color (0.9f, 0.5f, 1f);
	private Color TRIANGLE = new Color (0.35f,0.04f,1f);
	private Color TRIANGLE_GLOW = new Color (0.04f, 0.58f, 1f);
	private Color CIRCLE = new Color (0,0.73f,0.54f);
	private Color CIRCLE_GLOW = new Color (0, 1f, 0.81f);

	void Start () 
	{
		glowParticles = (GameObject)Instantiate(glowParticles);
		glowParticles.transform.position = new Vector3 (this.transform.position.x,this.transform.position.y, this.transform.position.z+0.5f );
		glowParticles.transform.parent = this.transform;
	}

	void FixedUpdate ()
	{
		if ( _lerpMaterial ) { Lerp_Color(); }
	}

	public void Set_Color (ShapeType shape) 
	{
		if (shape == ShapeType.Circle ) 	{ _colorFrom = renderer.material.color; _colorTo = CIRCLE; _glowFrom = glowParticles.particleSystem.startColor; _glowTo = CIRCLE_GLOW; }
		if (shape == ShapeType.Square ) 	{ _colorFrom = renderer.material.color; _colorTo = SQUARE; _glowFrom = glowParticles.particleSystem.startColor; _glowTo = SQUARE_GLOW; }
		if (shape == ShapeType.Triangle ) 	{ _colorFrom = renderer.material.color; _colorTo = TRIANGLE; _glowFrom = glowParticles.particleSystem.startColor; _glowTo = TRIANGLE_GLOW; }
		if (shape == ShapeType.Hexagon ) 	{ _colorFrom = renderer.material.color; _colorTo = PENTAGON; _glowFrom = glowParticles.particleSystem.startColor; _glowTo = PENTAGON_GLOW; }

		_lerpMaterial = true;

		glowParticles.particleSystem.Stop();
	}

	void Lerp_Color ()
	{
		_lerpTimer += Time.deltaTime;
		 
		this.renderer.material.color = Color.Lerp(_colorFrom,_colorTo,_lerpTimer);
		glowParticles.particleSystem.startColor = Color.Lerp(_glowFrom,_glowTo,_lerpTimer);

		if (_lerpTimer > 1)
		{
			glowParticles.particleSystem.Play();
			_lerpMaterial = false;
			_lerpTimer = 0;
		}
	}
}
