using UnityEngine;

public interface IPaddleInput
{
    public PaddleSettings PaddleSettings { get; set; }
    public Vector2 GetInput();
}
