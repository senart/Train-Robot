using UnityEngine;

[AddComponentMenu("Scripts/ DragItem")]

public class DragItem : UIDragDropItem
{
	public GameObject prefab, cubeDummy, prefabDummy;
	private float offset = 0.5F, eps = 0.01f;  //Cube wall divided by two
	int layerMask;

	void Start ()
	{
		base.Start ();
		layerMask = LayerMask.NameToLayer ("Default");
		cubeDummy.SetActive (false);
		prefabDummy.SetActive (false);
	}

	void ChangeVisibility (GameObject obj, bool visibility)
	{
		if (transform.GetChild (0))
			transform.GetChild (0).gameObject.SetActive (visibility);
	}

	protected override void OnDragDropMove (Vector2 delta)
	{
		base.OnDragDropMove (delta);
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, 100)) {
			Vector3 worldPosition = hit.point;
			Vector3 targetTransform = hit.collider.gameObject.transform.position;
			float x = targetTransform.x;
			float y = targetTransform.y;
			float z = targetTransform.z;
			Transform trans = cubeDummy.transform;

			if (!(worldPosition.y < -offset + eps)) {

				if (Mathf.Abs (worldPosition.x - (x + offset)) < eps) {
					targetTransform += new Vector3 (2 * offset, 0, 0);
					trans.rotation = Quaternion.Euler (new Vector3 (0, 90, 0));
				} else if (Mathf.Abs (worldPosition.y - (y + offset)) < eps) {
					targetTransform += new Vector3 (0, 2 * offset, 0);
				} else if (Mathf.Abs (worldPosition.z - (z + offset)) < eps) {
					targetTransform += new Vector3 (0, 0, 2 * offset);
				} else if (Mathf.Abs (worldPosition.x - (x - offset)) < eps) {
					targetTransform += new Vector3 (- 2 * offset, 0, 0);
					trans.rotation = Quaternion.Euler (new Vector3 (0, -90, 0));
				} else if (Mathf.Abs (worldPosition.y - (y - offset)) < eps) {
					targetTransform += new Vector3 (0, 0 - 2 * offset, 0);
				} else if (Mathf.Abs (worldPosition.z - (z - offset)) < eps) {
					targetTransform += new Vector3 (0, 0, - 2 * offset);
					trans.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
				}
			
				cubeDummy.transform.position = targetTransform;
				prefabDummy.transform.position = targetTransform;
				prefabDummy.transform.rotation = trans.rotation;
				cubeDummy.SetActive (true);
				prefabDummy.SetActive (true);
				ChangeVisibility (gameObject, false);
			}
		} else {
			if (cubeDummy)
				cubeDummy.SetActive (false);
			if (prefabDummy)
				prefabDummy.SetActive (false);
			ChangeVisibility (gameObject, true);
		}

	}

	protected override void OnDragDropRelease (GameObject surface)
	{
		if (prefabDummy.activeSelf) {
			GameObject child = NGUITools.AddChild (GameObject.Find ("Player"), prefab);
			child.transform.position = prefabDummy.transform.position;
			child.transform.rotation = prefabDummy.transform.rotation;
			child.GetComponent<Chosen> ().enabled = true;
			child.GetComponent<Collider> ().enabled = true;
			cubeDummy.SetActive (false);
			prefabDummy.SetActive (false);

			// Destroy this NGUI drag icon as it's no longer needed
			NGUITools.Destroy (gameObject);
			return;
		}

		base.OnDragDropRelease (surface);
	}

}
			                                  