using UnityEngine;

namespace GoblinShared
{
	/// <summary> Interface for the Goblin character root controller </summary>
	public interface IGoblinController
	{
		GoblinCustomisationDefinition customisationDefinition { get; }
		Animator bodyAnimator { get; }
	}
}
