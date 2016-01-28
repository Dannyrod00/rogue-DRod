using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardController : MonoBehaviour {

	public int columns;
	public int rows;

	public GameObject[] floors;
	public GameObject[] outerWalls;
	public GameObject[] wallObstacles;
	public GameObject[] foodItems;
	public GameObject[] enemies;
	public GameObject exit;

	private Transform gameBoard;
	private List<Vector3> obstacleGrid;

	void Awake () {
		obstacleGrid = new List<Vector3>();
	}
	
	void Update () {
	
	}

	private void InitializeObstaclePositions(){
		obstacleGrid.Clear ();

		for (int x = 2; x < columns -2; x++) {
			for(int y = 2; y < rows -2; y++){
				obstacleGrid.Add(new Vector3(x, y, 0f));
			}
		}
	}

	private void SetUpGameBoard(){
		gameBoard = new GameObject("Game Board").transform;

		for(int x = 0; x < columns; x++){
			for(int y = 0; y < rows; y++){
				GameObject selectedTile;
				if(x == 0 || y == 0 || x == columns - 1 || y == rows - 1){
				selectedTile = outerWalls[Random.Range(0,outerWalls.Length)];
				}else{
				selectedTile = floors[Random.Range(0, floors.Length)];
				}
				GameObject floorTile = (GameObject)Instantiate(selectedTile, new Vector3(x, y, 0f), Quaternion.identity);
				floorTile.transform.SetParent(gameBoard);
			}

		}

	}

	public void SetRandomObstaclesOnGrid(GameObject[] obstaclesArray, int minimum, int maximum){
		int obstacleCount = Random.Range(minimum, maximum +1);

		if(obstacleCount > obstacleGrid.Count){
			obstacleCount = obstacleGrid.Count;

		}

		for (int index = 0; index < obstacleCount; index++) {
			GameObject selectedObstacle = obstaclesArray[Random.Range(0, obstaclesArray.Length)];
			Instantiate(selectedObstacle, SelectGridPosition(), Quaternion.identity);
		
		}
	}

	private Vector3 SelectGridPosition(){
		int randomIndex = Random.Range (0, obstacleGrid.Count);
		Vector3 randomPosition = obstacleGrid[randomIndex];
		obstacleGrid.RemoveAt (randomIndex);
		return randomPosition;
	
	}

	public void SetupLevel(){
		InitializeObstaclePositions ();
		SetUpGameBoard();
		SetRandomObstaclesOnGrid (wallObstacles, 3, 9);
		SetRandomObstaclesOnGrid (foodItems, 1, 5);
		SetRandomObstaclesOnGrid (enemies, 2, 5);
		Instatiate (exit, Vector3 (colums - 2, rows - 2, 0f),Quaternion.identity);

	}

}
