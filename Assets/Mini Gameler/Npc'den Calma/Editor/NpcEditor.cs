using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Npc))]
public class NpcEditor : Editor
{
    private void OnSceneGUI()
    {
        Npc npc = (Npc)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(npc.transform.position, Vector3.up, Vector3.forward, 360, npc.npcGorusUzaklığı);

        Vector3 gorusAci1 = GorusSinirCiz(npc.transform.eulerAngles.y, -npc.npcGorusGenislik / 2);
        Vector3 gorusAci2 = GorusSinirCiz(npc.transform.eulerAngles.y, npc.npcGorusGenislik / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(npc.transform.position, npc.transform.position + gorusAci1 * npc.npcGorusUzaklığı);
        Handles.DrawLine(npc.transform.position, npc.transform.position + gorusAci2 * npc.npcGorusUzaklığı);

        if (npc.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(npc.transform.position, npc.player.transform.position);
        }
    }
    private Vector3 GorusSinirCiz(float eularY, float angle)
    {
        angle += eularY;

        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}