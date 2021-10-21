using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
	/// <summary>
	/// Shows debug lines in scene view to help debug issues with creating steering behaviours.
	/// NOTE: [field: SerializeField] exposes a C# property to Unity's inspector which is useful to toggle at runtime
	/// </summary>
	[field: SerializeField]
	public bool ShowDebugLines { get; protected set; } = true;

	protected Vector3 desiredVelocity;
	protected Vector3 steeringVelocity;

	/// <summary>
	/// Do steering behaviour code here. At the end of this the desiredVelocity and steeringVelocity variables should be set
	/// </summary>
	/// <param name="steeringAgent">The agent this component is acting on</param>
	/// <returns>The steeringVelocity should always be returned here</returns>
	public abstract Vector3 UpdateBehaviour(SteeringAgent steeringAgent);

	protected virtual void Start()
	{
		// Annoyingly this is needed for the enabled bool to work in Unity. A MonoBehaviour must now have one of the following
		// to activate this: Start(), Update(), FixedUpdate(), LateUpdate(), OnGUI(), OnDisable(), OnEnabled()
	}

	/// <summary>
	/// Draws debug info that is helpful to see what might be happening
	/// </summary>
	public virtual void DebugDraw(SteeringAgent steeringAgent)
	{
		Debug.DrawRay(transform.position, desiredVelocity, Color.red);
		Debug.DrawRay(transform.position, steeringAgent.CurrentVelocity, Color.green);
		Debug.DrawRay(transform.position + steeringAgent.CurrentVelocity, steeringVelocity, Color.blue);
	}

	/// <summary>
	/// Draws a circle fused in debugging
	/// </summary>
	/// <param name="position">Position of centre of circle</param>
	/// <param name="radius">Radius of the circle</param>
	/// <param name="lineCount">Number of lines used to draw the circle (More lines = smoother circle)</param>
	public virtual void DebugDrawCircle(Vector3 position, float radius, int lineCount = 12)
	{
		for(int lineIndex = 0; lineIndex < lineCount; ++ lineIndex)
		{
			float firstAngle = ((float)lineIndex / (float)lineCount) * (2.0f * Mathf.PI);
			float secondAngle = ((float)(lineIndex + 1) / (float)lineCount) * (2.0f * Mathf.PI);
			Vector3 firstPoint = new Vector3(Mathf.Cos(firstAngle), 0.0f, Mathf.Sin(firstAngle)) * radius;
			Vector3 secondPoint = new Vector3(Mathf.Cos(secondAngle), 0.0f, Mathf.Sin(secondAngle)) * radius;
			Debug.DrawLine(firstPoint + position, secondPoint + position, Color.magenta);
		}
	}
}