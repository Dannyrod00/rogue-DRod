﻿using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	public float moveTime = 0.50f;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rigidBody;
	private LayerMask collisionLayer;
	private float inverseMoveTime;


	protected virtual void Start () {
		boxCollider = GetComponent<BoxCollider2D>();
		rigidBody = GetComponent<Rigidbody2D>();
		collisionLayer = LayerMask.GetMask("collision Layer");
		inverseMoveTime = 0.50f / moveTime;
	
	}

	protected bool canObjectMove(int xDirection, int yDirection){
		Vector2 startPosition = rigidBody.position;
		Vector2 endPosition = startPosition + new Vector2 (xDirection, yDirection);

		boxCollider.enabled = false;
		RaycastHit2D hit = Physics2D.Linecast(startPosition, endPosition, collisionLayer);
		boxCollider.enabled = true;

		if(hit.transform == null){
			StartCoroutine(SmoothMovementRoutine(endPosition));
			return true;
		}

		return false;
	}

	protected IEnumerator SmoothMovementRoutine(Vector2 endPosition) {
		float remainingDistanceToEndPosition;

		do{
			remainingDistanceToEndPosition = (rigidBody.position - endPosition).sqrMagnitude;
			Vector2 updatedPosition = Vector2.MoveTowards(rigidBody.position, endPosition, inverseMoveTime * Time.deltaTime);
			rigidBody.MovePosition(updatedPosition);
			yield return null;
		}while(remainingDistanceToEndPosition > float.Epsilon);
	
	}

	void Update () {
	
	}
}
