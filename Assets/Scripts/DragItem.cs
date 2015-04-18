using UnityEngine;

[AddComponentMenu("Scripts/ DragItem")]

public class DragItem : UIDragDropItem
{
	public GameObject prefab;
	private float offset = 0.5F;  //Cube wall divided by two

	protected override void OnDragDropRelease (GameObject surface)
	{
		if (surface.GetComponent<Chosen> () != null) {
			GameObject child = NGUITools.AddChild (GameObject.Find ("Player"), prefab);
			child.transform.localScale = surface.transform.localScale;
			
			Transform trans = child.transform;
			float x = surface.transform.position.x;
			float y = surface.transform.position.y;
			float z = surface.transform.position.z;
			
			//!!!!!
			//THIS MAKES A BUG, SOMETIMES RESETING THE NEWLY PLACED CUBE TO COORDINATES 0,0,0
			//Maybe solved with switch statement?
			//Place cubes next to each other
			if (UICamera.lastWorldPosition.x == x + offset) {
				trans.position = new Vector3 (x + 2 * offset, y, z);
				trans.rotation = Quaternion.Euler (new Vector3 (0, 90, 0));
			} if (UICamera.lastWorldPosition.y == y + offset) {
				trans.position = new Vector3 (x, y + 2 * offset, z);
			} if (UICamera.lastWorldPosition.z == z + offset) {
				trans.position = new Vector3 (x, y, z + 2 * offset);
			} if (UICamera.lastWorldPosition.x == x - offset) {
				trans.position = new Vector3 (x - 2 * offset, y, z);
				trans.rotation = Quaternion.Euler (new Vector3 (0, -90, 0));
			} if (UICamera.lastWorldPosition.y == y - offset) {
				trans.position = new Vector3 (x, y - 2 * offset, z);
			} if (UICamera.lastWorldPosition.z == z - offset) {
				trans.position = new Vector3 (x, y, z - 2 * offset);
				trans.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
			} //else {
			//trans.position = new Vector3 (x, y + 2 * offset, z);   //BUG - this shouldn't be here
			//}
			
			// Destroy this NGUI drag icon as it's no longer needed
			NGUITools.Destroy (gameObject);
			return;
		}
		base.OnDragDropRelease (surface);
	}
}
			                                  