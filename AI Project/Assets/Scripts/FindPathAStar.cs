using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathMarker
{
    public MapLocation location;
    public float G;
    public float H;
    public float F;
    public GameObject marker;
    public PathMarker parent;
    
    public PathMarker(MapLocation l,float g,float h,float f, GameObject marker,PathMarker p)
    {
        location = l;
        G = g;
        H = h;
        F = f;
        this.marker = marker;
        parent = p;

    }

    public override bool Equals(object obj)
    {
        if (obj == null || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }

        else
        {
            return location.Equals(((PathMarker)obj).location);
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }
}
public class FindPathAStar : MonoBehaviour
{
    public Maze maze;
    public Material openMaterial;
    public Material closedMaterial;

    public GameObject start;
    public GameObject end;
    public GameObject pathPoint;

    private List<PathMarker> closedList = new List<PathMarker>();
    private List<PathMarker> openList = new List<PathMarker>();

    private PathMarker goalNode;
    private PathMarker startNode;
    private PathMarker lastPos;

    private bool isDone = false;

    void RemoveAllMarkers()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");

        foreach (GameObject marker in markers)
        {
            Destroy(marker);
        }
        
    }

    void BeginSearch()
    {
        isDone = false;
        RemoveAllMarkers();

        List<MapLocation> locations = new List<MapLocation>();

        for (int z = 1; z < maze.depth -1 ; z++)
        {
            for (int x = 1; x < maze.width -1; x++)
            {
                if (maze.map[x,z] != 1)
                {
                    locations.Add(new MapLocation(x,z));
                }
            }
        }
        
        locations.Shuffle();

        Vector3 startLocation = new Vector3(locations[0].x * maze.scale, 0, locations[0].z * maze.scale);
        
        startNode = new PathMarker(new MapLocation(locations[0].x, locations[0].z), 0, 0,0,
            Instantiate(start, startLocation, Quaternion.identity),null);
        
        Vector3 goalLocation = new Vector3(locations[1].x * maze.scale, 0, locations[1].z * maze.scale);
        
        goalNode = new PathMarker(new MapLocation(locations[1].x, locations[1].z), 0, 0,0,
            Instantiate(end, goalLocation, Quaternion.identity),null);


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BeginSearch();
        }
    }
}
