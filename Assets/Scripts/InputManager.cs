using UnityEngine;
using System.Collections;

public enum MoveDirection
{
	Left, Right, Up, Down
}


public class InputManager : MonoBehaviour {
	
	private GameManager gm;

	void Awake() // контакт с GameManager
	{
		gm = GameObject.FindObjectOfType<GameManager> ();
	}

	void Start () {
	
	}
	
	void Update () 
	{
		if (gm.State == GameState.Playing)
		{
			if (Input.GetKeyDown (KeyCode.RightArrow)) 
			{
				// Вправо
				gm.Move(MoveDirection.Right);
			} 
			else if (Input.GetKeyDown (KeyCode.LeftArrow)) 
			{
				// Влево
				gm.Move(MoveDirection.Left);
			}
			else if (Input.GetKeyDown (KeyCode.UpArrow)) 
			{
				// Вверх
				gm.Move(MoveDirection.Up);
			}
			else if (Input.GetKeyDown (KeyCode.DownArrow)) 
			{
				// Вниз
				gm.Move(MoveDirection.Down);
			}
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Выход из игры на ПК
                Application.Quit();
            }
        }
	
	}
}
