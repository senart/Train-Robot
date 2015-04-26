using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {
	//this is the flag/counter indicate how many action are on-going
	//no new action should be able to start as long as this is not clear(>0)
	private static int actionInProgress;
	private static int currentTurn;

	private static TurnManager instance;
	private static List<RobotBrain> players;

//	public static void AddPlayer(RobotBrain player)
//	{
//		players.Add (player);
//	}
//
//	public static void NextUnit(){
//		//end this turn
//	}
//
//	public static void EndTurn()
//	{
//
//	}

	//called by all to check if a new action can take place (shoot, move, ability, etc)
	public static bool ClearToProceed(){
		return actionInProgress == 0;
	}

	//called to indicate that an action has been started, prevent any other action from starting
	public static void ActionCommenced(){
		actionInProgress+=1;
	}

	//called to indicate that an action has been completed
	public static void ActionCompleted(){
		actionInProgress=Mathf.Max(0, actionInProgress-=1);
	}
}
