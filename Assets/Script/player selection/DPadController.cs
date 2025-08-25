using UnityEngine;

public class DPadController : MonoBehaviour
{
    private PlayerMovement player;
    public void SetPlayer(PlayerMovement p) { player = p; }

    public void PressUp()      { if (player) player.PressUp(); }
    public void ReleaseUp()    { if (player) player.ReleaseUp(); }
    public void PressDown()    { if (player) player.PressDown(); }
    public void ReleaseDown()  { if (player) player.ReleaseDown(); }
    public void PressLeft()    { if (player) player.PressLeft(); }
    public void ReleaseLeft()  { if (player) player.ReleaseLeft(); }
    public void PressRight()   { if (player) player.PressRight(); }
    public void ReleaseRight() { if (player) player.ReleaseRight(); }
}
