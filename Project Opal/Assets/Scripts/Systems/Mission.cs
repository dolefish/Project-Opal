 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission {

    public bool started;
    public bool finished;

    public int id;
    public int stage;
    public int branch;   

    public string title;
    public string body;
    public string[] goals;

    public Mission()
    {

    }

    public Mission(int id, bool started, bool finished, int stage, int branch, string title, string body, params string[] goals)
    {
        this.id = id;
        this.started = started;
        this.finished = finished;
        this.stage = stage;
        this.branch = branch;
        this.title = title;
        this.body = body;
        this.goals = goals;
    }
}
