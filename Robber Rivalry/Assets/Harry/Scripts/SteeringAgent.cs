using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
	/// <summary>
	/// Returns the maximum speed the agent can have
	/// NOTE: [field: SerializeField] exposes a C# property to Unity's inspector which is useful to toggle at runtime
	/// </summary>
	[field: SerializeField]
	public float MaxSpeed { get; protected set; } = 400.0f;

	/// <summary>
	/// Returns the maximum steering amount that can be applied
	/// NOTE: [field: SerializeField] exposes a C# property to Unity's inspector which is useful to toggle at runtime
	/// </summary>
	[field: SerializeField]
	public float MaxSteering { get; protected set; } = 100.0f;

	/// <summary>
	/// Returns the current velocity of the Agent
	/// NOTE: [field: SerializeField] exposes a C# property to Unity's inspector which is useful to toggle at runtime
	/// </summary>
	public Vector3 CurrentVelocity	{ get; protected set; }

	/// <summary>
	/// Stores a list of all steering behaviours that are on a SteeringAgent GameObject, regardless if they are enabled or not
	/// </summary>
	private List<SteeringBehaviour> steeringBehvaiours = new List<SteeringBehaviour>();

	/// <summary>
	/// Called once per frame
	/// </summary>
	private void Update()
	{
		CooperativeArbitration();
		UpdatePosition();
		UpdateDirection();
	}

	/// <summary>
	/// This is responsible for how to deal with multiple behaviours and selecting which ones to use. Please see this link for some decent descriptions of below:
	/// https://alastaira.wordpress.com/2013/03/13/methods-for-combining-autonomous-steering-behaviours/
	/// Remember some options for choosing are:
	/// 1 Finite state machines which can be part of the steering behaviours or not (Not the best approach but quick)
	/// 2 Weighted Truncated Sum
	/// 3 Prioritised Weighted Truncated Sum
	/// 4 Prioritised Dithering
	/// 5 Context Behaviours: https://andrewfray.wordpress.com/2013/03/26/context-behaviours-know-how-to-share/
	/// 6 Any other approach you come up with
	/// </summary>
	protected virtual void CooperativeArbitration()
	{
		Vector3 steeringVelocity = Vector3.zero;
		
		GetComponents<SteeringBehaviour>(steeringBehvaiours);
		foreach (SteeringBehaviour currentBehaviour in steeringBehvaiours)
		{
			if(currentBehaviour.enabled)
			{
				steeringVelocity += currentBehaviour.UpdateBehaviour(this);

				// Show debug lines in scene view
				if (currentBehaviour.ShowDebugLines)
				{
					currentBehaviour.DebugDraw(this);
				}
			}
		}

		// Set final velocity
		CurrentVelocity += Helper.LimitVector(steeringVelocity, MaxSteering);
		CurrentVelocity = Helper.LimitVector(CurrentVelocity, MaxSpeed);
	}

	/// <summary>
	/// Updates the position of the GAmeObject via Teleportation. In Craig Reynolds architecture this would the Locomotion layer
	/// </summary>
	protected virtual void UpdatePosition()
	{
		transform.position += CurrentVelocity * Time.deltaTime;

		// The code below is just to wrap the screen for the agent like in Asteroids for example
		Vector3 position = transform.position;
		Vector3 viewportPosition = Camera.main.WorldToViewportPoint(position);

		while(viewportPosition.x < 0.0f)
		{
			viewportPosition.x += 1.0f;
		}
		while (viewportPosition.x > 1.0f)
		{
			viewportPosition.x -= 1.0f;
		}
		while (viewportPosition.y < 0.0f)
		{
			viewportPosition.y += 1.0f;
		}
		while (viewportPosition.y > 1.0f)
		{
			viewportPosition.y -= 1.0f;
		}

		position = Camera.main.ViewportToWorldPoint(viewportPosition);
		position.y = 1.5f;
		transform.position = position;
	}

	/// <summary>
	/// Sets the direction of the triangle to the direction it is moving in to give the illusion it is turning. Try taking out the function
	/// call in Update() to see what happens
	/// </summary>
	protected virtual void UpdateDirection()
	{
		// Don't set the direction if no direction
		if (CurrentVelocity.sqrMagnitude > 0.0f)
		{
			transform.forward = Vector3.Normalize(new Vector3(CurrentVelocity.x, 0.0f, CurrentVelocity.z));
		}
	}
}
