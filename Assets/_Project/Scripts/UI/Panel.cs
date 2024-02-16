using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    protected CanvasController canvasController;

    public abstract void SetInteractables(bool state);

    public void Init(CanvasController controller) => canvasController = controller;

}
