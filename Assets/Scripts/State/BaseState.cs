using UnityEngine;
using System;

public abstract class BaseState 
{
    public BaseState(GameObject gameobj)
    {
        this.gameobj = gameobj;
        this.transform = gameobj.transform;
    }
    //only accsesible by the states classes
    protected GameObject gameobj;
    protected Transform transform;

    public abstract Type Tick();
}
