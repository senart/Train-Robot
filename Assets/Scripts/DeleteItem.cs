using UnityEngine;

[AddComponentMenu("Scripts/ DeleteItem")]
public class DeleteItem : MonoBehaviour
{
	void OnClick ()
	{
		GameObject.FindGameObjectWithTag ("Cube Frame").transform.parent.GetComponent<Chosen> ().OnDoubleClick ();
	}
}
