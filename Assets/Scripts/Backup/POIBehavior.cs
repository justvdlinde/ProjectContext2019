using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Placeholder for UniqueIdDrawer script
public class UniqueIdentifierAttribute : PropertyAttribute { }

public class POIBehavior : MonoBehaviour
{
    [UniqueIdentifier] public string uniqueId = string.Empty;

    public POIStatus currentStatus;
    public Dictionary<string, POIView> poiContainer = new Dictionary<string, POIView>();

    protected POIView requiredToPlay = null;

    private StoryList storyList;

    public bool CanStartStory ()
    {
        return false;
    }
}
