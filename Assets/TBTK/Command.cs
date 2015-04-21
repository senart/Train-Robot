using UnityEngine;
using System.Collections;

public class Command : MonoBehaviour {
	private int ID;

	public Command(int ID){
		this.ID = ID;
	}

	public int GetID()
	{
		return ID;
	}

	public virtual void Execute(){
		//Nothing
	}

	protected virtual void OnDrag(){
		if (transform.parent != null)
			transform.parent.GetComponent<Command>().RemoveFromCommands (ID);
	}

	protected virtual void RemoveFromCommands (int ID){
		//Nothing
	}
}
