namespace GoblinShared
{
    public static partial class Extensions
    {
        public static void DestroySafe(this UnityEngine.Object o)
        {
            if (!o) return;
            if (UnityEngine.Application.isPlaying) UnityEngine.Object.Destroy(o);
            else UnityEngine.Object.DestroyImmediate(o);
        }
    }
}
