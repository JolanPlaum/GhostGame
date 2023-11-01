using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRockBehavior : MovementBehavior
{
	protected override void HandleMovement()
	{
		// Since a rock's X/Z position is frozen in rigidbody,
		//  manually move the rigidbody in the desired direction
		float travelDistance = _movementSpeed * Time.deltaTime;
		_rigidbody.transform.position += DesiredMovementDirection * travelDistance;
	}
}
