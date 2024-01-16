using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public List<Stage> stages;

    public int currentStageIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (stages == null)
        {
            stages = new List<Stage>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Stage GetCurrentStage()
    {
        return stages[currentStageIndex];
    }
}
