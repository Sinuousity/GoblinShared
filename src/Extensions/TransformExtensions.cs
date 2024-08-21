using UnityEngine;

namespace GoblinShared
{
    public static partial class Extensions
    {
        public static Transform FindRecursive(this Transform root, string name)
        {
            if (root.gameObject.name == name) return root;
            foreach (Transform child in root) { if (child.gameObject.name == name) return child; }
            foreach (Transform child in root)
            {
                var res = child.FindRecursive(name);
                if (res) return res;
            }
            return null;
        }

        public static Transform[] GetAllChildren(this Transform root)
        {
            if (!root) return new Transform[0];
            return root.GetComponentsInChildren<Transform>();
        }
    }
}
