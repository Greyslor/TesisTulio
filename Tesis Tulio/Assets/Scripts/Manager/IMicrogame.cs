using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMicrogame
{
    void StartGame();
    void EndGame();
    bool IsCompleted { get; }
    bool IsFailed { get; }
}

