using UnityEngine;

public static class Helper
{
	/// <summary>
	/// Limits the vector to the specified magnitude
	/// </summary>
	/// <param name="vector">Vector to limit</param>
	/// <param name="magnitude">Amount to limit to</param>
	/// <returns>New Vector that has been limited</returns>
	static public Vector3 LimitVector(Vector3 vector, float magnitude)
	{
		// This limits the velocity to max speed. sqrMagnitude is used rather than magnitude as in magnitude a square root must be computed which is a slow operation.
		// By using sqrMagnitude and comparing with maxSpeed squared we can get around using the expensive square root operation.
		if (vector.sqrMagnitude > magnitude * magnitude)
		{
			vector.Normalize();
			vector *= magnitude;
		}
		return vector;
	}

	/// <summary>
	/// Returns the mouse position in 2d space
	/// </summary>
	/// <returns>The mouse position in 2d space</returns>
	static public Vector3 GetMousePosition()
	{
		Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return new Vector3(temp.x, temp.y, 0.0f);
	}
}
