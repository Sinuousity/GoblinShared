using UnityEngine;

namespace GoblinShared
{
    [CreateAssetMenu(menuName = "GoblinView/Definition/EquipItem Group")]
    public class EquipItemGroup : ScriptableObject
    {
        public EquipItemDefinition[] items = new EquipItemDefinition[0];
    }
}