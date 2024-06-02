/// <summary>
/// Reacts to continuous interaction. 
/// </summary>
internal interface IInteractableHold : IInteractable{
    /// <summary>
    /// Called every frame the object is being interacted with.
    /// </summary>
    public void InteractHold();
}