/// <summary>
/// Reacts to start of interactions.
/// </summary>
internal interface IInteractableStart : IInteractable{
    /// <summary>
    /// Called the first frame the object is being interacted with.
    /// </summary>
    public void InteractStart();
}