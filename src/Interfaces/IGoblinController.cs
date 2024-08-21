using UnityEngine;

namespace GoblinShared
{
	/// <summary> Interface for the Goblin character root controller </summary>
	public interface IGoblinController
	{
		bool IsValid { get; }
		bool IsCreated { get; }
		GoblinCustomisationDefinition customisationDefinition { get; }
		Animator bodyAnimator { get; }
	}
}
