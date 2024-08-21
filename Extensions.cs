using UnityEngine;

namespace GoblinShared
{
    public static partial class Extensions
    {
        public static void DestroySafe(this Object o)
        {
            if (!o) return;
            if (Application.isPlaying) Object.Destroy(o);
            else Object.DestroyImmediate(o);
        }

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

        public static Color SetRed(this Color c, float red) => new Color(red, c.g, c.b, c.a);
        public static Color SetGreen(this Color c, float green) => new Color(c.r, green, c.b, c.a);
        public static Color SetBlue(this Color c, float blue) => new Color(c.r, c.g, blue, c.a);
        public static Color SetAlpha(this Color c, float alpha) => new Color(c.r, c.g, c.b, alpha);
    }
}
