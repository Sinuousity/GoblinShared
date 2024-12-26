namespace GoblinShared
{
    public static partial class Extensions
    {
        /// <summary>
        /// Avoids HideFlags.DontSave ( which uses HideFlags.DontUnloadUnusedAsset ) and instead uses HideFlags.DontSaveInBuild | HideFlags.DontSaveInEditor. 
        /// Also uses HideFlags.NotEditable when 'editable' is false
        /// </summary>
        public static void SetRuntimeFlags(this UnityEngine.Object o, bool editable = false, bool hideInHierarchy = false)
        {
            if (!o) return;
            o.hideFlags = UnityEngine.HideFlags.DontSaveInBuild | UnityEngine.HideFlags.DontSaveInEditor;
            if (!editable) o.hideFlags |= UnityEngine.HideFlags.NotEditable;
            if (hideInHierarchy) o.hideFlags |= UnityEngine.HideFlags.HideInHierarchy;
        }

        public static void DestroySafe(this UnityEngine.Object o)
        {
            if (!o) return;
            if (UnityEngine.Application.isPlaying) UnityEngine.Object.Destroy(o);
            else UnityEngine.Object.DestroyImmediate(o);
        }
    }
}
