using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// An undirected Graph structure for finding the articulation modules
public class Graph : MonoBehaviour
{
	public List<int> ArticulationPoints;
	//public List<GameObject> Modules;

	private int numVert; //Number of vertices
	private List<List<int>> adj;  //Adjecency list
	private int time=0;

	void Awake()
	{
		numVert = 0;
		adj = new List<List<int>> ();
		ArticulationPoints = new List<int> ();
	}

	public int Count()
	{
		return numVert;
	}
	
	public void AddVertex(int ID, Vector3 pos)
	{
		adj.Add(new List<int>());
		numVert++;
		foreach (int surroundID in SurroundingModuleIDs(pos)) {
			AddEdge (ID, surroundID);
		}
		UpdateArticulationPoints();
	}

	public void RemoveVertex(int ID)
	{
		foreach (List<int> connections in adj) {
			connections.Remove(ID);
			for(int i = 0; i < connections.Count; i++)
				if(connections[i] > ID) connections[i]--;
		}
		adj.Remove (adj [ID]);
		numVert--;
		foreach (Transform module in transform.GetComponentInChildren<Transform>()) {
			if(  module.GetComponent<Chosen>().ID > ID)  module.GetComponent<Chosen>().ID--;
		}
		UpdateArticulationPoints ();
	}

	public void AddEdge(int x, int y)
	{
		if (adj[x]!=null && adj[y]!=null && !adj [x].Contains (y) && !adj [y].Contains (x)) {
			//Undrected Graph
			adj [x].Add (y);  
			adj [y].Add (x);
		}
	}

	private List<int> SurroundingModuleIDs(Vector3 pos)
	{
		List<int> surrounding = new List<int> ();
		
		foreach (Transform module in transform.GetComponentInChildren<Transform>()) {
			if (module.GetComponent<Chosen> ()) {
				if ((module.position == pos + Vector3.up) ||
					(module.position == pos + Vector3.down) ||
					(module.position == pos + Vector3.left) ||
					(module.position == pos + Vector3.right) ||
					(module.position == pos + Vector3.forward) ||
					(module.position == pos + Vector3.back))
					surrounding.Add (module.GetComponent<Chosen> ().ID);
			}
		}
		return surrounding;
	}

	public void UpdateArticulationPoints()
	{
		ArticulationPoints.Clear ();
		bool[] visited = new bool[numVert];
		int[] disc = new int[numVert];
		int[] low = new int[numVert];
		int[] parent = new int[numVert];
		bool[] ap = new bool[numVert];
		time = 0;

		for (int i = 0; i < numVert; i++) {
			parent[i] = -1;
		}

		for (int i = 0; i < numVert; i++) {
			if(!visited[i]) ArtPoint(i,visited,disc,low,parent,ap);
		}

		for (int i = 0; i < numVert; i++) {
			if(ap[i]) ArticulationPoints.Add(i);
		}
	}

	private void ArtPoint(int u, bool[] visited, int[] disc, int[] low, int[] parent, bool[] ap)
	{
		int children = 0;
		visited [u] = true;
		disc [u] = low [u] = ++time;

		foreach (int i in adj[u]) {
			int v = i;
			if(!visited[v])
			{
				children++;
				parent[v]=u;
				ArtPoint(v, visited, disc,low,parent,ap);

				low[u]=Mathf.Min(low[u],low[v]);
				if(parent[u] == -1 && children>1) ap[u] = true;
				if(parent[u] != -1 && low[v] >= disc[u]) ap[u] = true;
			}
			else if(v!=parent[u]) low[u] = Mathf.Min(low[u],disc[v]);
		}
	}
}

