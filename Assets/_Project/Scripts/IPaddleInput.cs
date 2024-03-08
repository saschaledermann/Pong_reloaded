using UnityEngine;

public interface IPaddleInput
{
    public PaddleSettings PaddleSettings { get; set; }
    public bool DoBoostShot { get; }
    public Vector2 GetInput();
}
