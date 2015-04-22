using UnityEngine;

[AddComponentMenu("Scripts/ DragItem")]

public class DragItem : UIDragDropItem
{
	public GameObject prefab;
	private float offset = 0.5F, eps = 0.01f;  //Cube wall divided by two


	protected override void OnDragDropRelease (GameObject surface)
	{
		Debug.Log(surface);
		if (prefab.GetComponent<Chosen> () != null) {
			int layerMask = LayerMask.NameToLayer("Default");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100)){
				Vector3 worldPosition = hit.point;
				Vector3 targetTransform = hit.collider.gameObject.transform.position;
				float x = targetTransform.x;
				float y = targetTransform.y;
				float z = targetTransform.z;
				Debug.Log(worldPosition);
				GameObject child = NGUITools.AddChild (GameObject.Find ("Player"), prefab);
				//child.transform.localScale = surface.transform.localScale;
				child.transform.localScale = Vector3.one;
				Transform trans = child.transform;

				//!!!!!
				//THIS MAKES A BUG, SOMETIMES RESETING THE NEWLY PLACED CUBE TO COORDINATES 0,0,0
				//Maybe solved with switch statement?
				//Place cubes next to each other
				if (Mathf.Abs(worldPosition.x - (x + offset))<eps) {
					targetTransform += new Vector3 (2 * offset, 0, 0);
					trans.rotation = Quaternion.Euler (new Vector3 (0, 90, 0));
				} if (Mathf.Abs(worldPosition.y - (y + offset))<eps) {
					targetTransform += new Vector3 (0, 2 * offset, 0);
				} if (Mathf.Abs(worldPosition.z - (z + offset))<eps) {
					targetTransform += new Vector3 (0, 0, 2 * offset);
				} if (Mathf.Abs(worldPosition.x - (x - offset))<eps) {
					targetTransform += new Vector3 (- 2 * offset, 0, 0);
					trans.rotation = Quaternion.Euler (new Vector3 (0, -90, 0));
				} if (Mathf.Abs(worldPosition.y - (y - offset))<eps) {
					targetTransform += new Vector3 (0, 0 - 2 * offset, 0);
				} if (Mathf.Abs(worldPosition.z - (z - offset))<eps) {
					targetTransform += new Vector3 (0, 0, - 2 * offset);
					trans.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
				}

				child.transform.position = targetTransform;
				
				// Destroy this NGUI drag icon as it's no longer needed
				NGUITools.Destroy (gameObject);
				return;
			}
			Debug.DrawRay(ray.origin, ray.direction, Color.cyan, 5);
		}
		base.OnDragDropRelease (surface);
	}

}
			                                  