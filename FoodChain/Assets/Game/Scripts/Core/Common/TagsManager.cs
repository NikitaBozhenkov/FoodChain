using UnityEngine;

namespace Game.Scripts.Common
{
    public static class TagsManager
    {
        public const string Animal = "Animal";
    }
    
    public static class LayersManager
    {
        public static int AnimalLayerMask => 1 << LayerMask.NameToLayer("Obstacle");
    }
}