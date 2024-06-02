/// <summary>
/// Reacts to interaction end.
/// </summary>
internal interface IInteractableEnd : IInteractable{
    /// <summary>
    /// Called on last frame of interaction.
    /// </summary>
    public void InteractEnd();
}