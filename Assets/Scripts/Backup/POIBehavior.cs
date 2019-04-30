using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Placeholder for UniqueIdDrawer script
public class UniqueIdentifierAttribute : PropertyAttribute { }

public class POIBehavior : MonoBehaviour
{
    [UniqueIdentifier] public string uniqueId;

    public status currentStatus;
    public Dictionary<string, POIView> poiContainer;
    public POIView requiredToPlay = null;

    private StoryList storyList;

    protected virtual void Start()
    {
        
    }

    public enum status
    {
        Undiscovered,
        Visited,
        Completed
    }

    public bool CanStartStory ()
    {
        return false;
    }
}
