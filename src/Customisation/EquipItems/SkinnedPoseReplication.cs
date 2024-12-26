using System.Collections.Generic;
using UnityEngine;

namespace GoblinShared
{
    public static class SkinnedPoseReplication
    {
        public static void CopyBoneAssignments(SkinnedMeshRenderer rendererA, SkinnedMeshRenderer rendererB)
        {
            if (!rendererA || !rendererB) return;

            rendererB.rootBone = rendererA.rootBone;
            var bonesNew = new Transform[rendererB.bones.Length];
            for (var ii = 0; ii < rendererB.bones.Length; ii++)
            {
                var oldBone = rendererB.bones[ii];
                for (var jj = 0; jj < rendererA.bones.Length; jj++)
                {
                    var nextBone = rendererA.bones[jj];
                    if (oldBone && nextBone && nextBone.name != oldBone.name) continue;
                    bonesNew[ii] = nextBone;
                }
            }

            rendererB.bones = bonesNew;
        }
    }
}