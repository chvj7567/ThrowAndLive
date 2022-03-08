using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum GameObjects
    {
        Unknown,
        Background,
        Map,
        Monster,
        Player,
    }
    public enum State
    {
        Idle,
        Run,
        Jump,
        Die,
    }
}
