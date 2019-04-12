using UnityEngine;

/// <summary>
/// Class for unity layers, can be selected via the inspector
/// </summary>
[System.Serializable]
public class Layer
{
    [SerializeField]
    private int m_LayerIndex = 0;
    public int LayerIndex
    {
        get { return m_LayerIndex; }
    }

    public void Set(int layerIndex)
    {
        if (layerIndex > 0 && layerIndex < 32)
        {
            m_LayerIndex = layerIndex;
        }
    }

    public int Mask
    {
        get { return 1 << m_LayerIndex; }
    }
}