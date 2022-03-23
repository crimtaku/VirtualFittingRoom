using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using UnityEngine.UI;

public class AdjustObjectLocation : MonoBehaviour
{
    public float x;
    public float y;
    public float z;

    public GameObject cloth1;
    public GameObject cloth2;
    public GameObject currentCloth;
    public GameObject[] obisolvers;

    public Text simulationValue;

    Vector3 newposition =new Vector3(0, - 10f, 0);

    //sets cloth 1 as default currentCloth
    private void Start()
    {
        currentCloth = cloth1;
    }

    //called once per frame, updates the position of currentCloth based on x, y and z values given by sliders
    void Update()
    {
        newposition.x = -x;
        newposition.y = y;
        newposition.z = z;

        currentCloth.transform.position=newposition;
    }
    //set x value from slider
    public void UpdateX(float newValue)
    {
        x = newValue;
    }

    //set y value from slider
    public void UpdateY(float newValue)
    {
        y = newValue;
    }

    //set z value from slider
    public void UpdateZ(float newValue)
    {
        z = newValue;
    }

    //changes currentCloth to correspond value given in the dropdown menu 
    public void Changeobject(int val)
    {
        if (val == 0)
        {
            currentCloth = cloth1;
        }
        if (val == 1)
        {
            currentCloth = cloth2;
        }
    }

    public void ChangeSimulationQuality(float val)
    {
        foreach(GameObject solverobject in obisolvers)
        {
            ObiLateFixedUpdater updater = solverobject.GetComponent<ObiLateFixedUpdater>();
            updater.substeps = (int) val;
        }
        simulationValue.text = val.ToString();
    }

    public void SimulateCloth()
    {
        foreach(GameObject solverobject in obisolvers)
        {
            ObiSolver solver = solverobject.GetComponent<ObiSolver>();
            solver.enabled = true;
        }
    }

    public void StopSimulation()
    {
        foreach (GameObject solverobject in obisolvers)
        {
            ObiSolver solver = solverobject.GetComponent<ObiSolver>();
            solver.enabled = false;
        }
    }
}
