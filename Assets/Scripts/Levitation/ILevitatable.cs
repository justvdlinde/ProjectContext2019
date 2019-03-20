public interface ILevitatable 
{
    System.Action DestroyEvent { get; set; }
    UnityEngine.Rigidbody Rigidbody { get; }
    LevitationManager LevitationManager { get; }
    bool IsLevitated { get; }

    void OnLevitateStart(LevitationManager levitationManager);
    void OnLevitateStop(LevitationManager levitationManager);
}
