using UnityEngine;

/// <summary>
/// 
/// </summary>
internal sealed class interactor : MonoBehaviour{
	
	[SerializeField, Range(0, 10), Tooltip("How far away can game objects be interacted with. ")] 
	private float interactRange = 2.5f;
	
    [SerializeField, Tooltip("Reference to the interacting objects transform. Relative interaction direction is forward. Typically this will be a camera.")] 
	private Transform interactorTransform;
	
	[SerializeField, Tooltip("The key to trigger interaction.")] 
	private KeyCode interactKey = KeyCode.F;
	
	/// <summary>
	/// The current object that is able to be interacted with in this frame.
	/// </summary>
    private IInteractable _interactable;
	
	/// <summary>
	/// The object that was interactable the last frame.
	/// </summary>
    private IInteractable _oldInteractable;

	/// <summary>
	/// The current hover text that should be displayed this frame.
	/// </summary>
    public string HoverText
    {
	    get
	    {
		    if (_interactable is null)
		    {
			    return "";
		    }

		    return _interactable.HoverText;
	    }
    }

    void Update()
    {
		_interactable = GetInteractableHit();

		// Interaction end (Player looks away)
		if (_oldInteractable != _interactable)
		{
			// Player is interacting
			if (Input.GetKey(interactKey) && _oldInteractable is IInteractableEnd oldEndInteractable)
			{
				oldEndInteractable.InteractEnd();
			}
			_oldInteractable = _interactable;
		}
		
	    if (_interactable == null)
	    {
		    return;
	    }

	    // Interaction start
	    if (Input.GetKeyDown(interactKey) && _interactable is IInteractableStart tapInteractable)
	    {
		    tapInteractable.InteractStart();
	    }

	    // Interaction hold
	    if (Input.GetKey(interactKey) && _interactable is IInteractableHold holdInteractable)
	    {
		    holdInteractable.InteractHold();
	    }
	    
	    // Interaction end (Player releases key)
	    if (Input.GetKeyUp(interactKey) && _interactable is IInteractableEnd endInteractable)
	    {
		    endInteractable.InteractEnd();
	    }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The interactable that is being looked at, and is in range.</returns>
    private IInteractable GetInteractableHit()
    {
	    Ray r = new Ray(interactorTransform.position, interactorTransform.forward);
	    bool didHit = Physics.Raycast(r, out RaycastHit hitInfo, interactRange);
	    if (!didHit)
	    {
		    return null;
	    }
	    hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable);
	    return interactable;
    }
}
