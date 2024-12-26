using System.Collections;
using UnityEngine;

public class RunningDirty
{
	public static long nowTicks => System.Diagnostics.Stopwatch.GetTimestamp();

	public float dirtyForSeconds => (float)System.TimeSpan.FromTicks(dirtyForTicks).TotalSeconds;
	public long dirtyForTicks => nowTicks - lastDirtyTicks;

	public bool isDirty { get; private set; }
	public long lastDirtyTicks { get; private set; }

	public float minimumRefreshDelay = 0.1f;
	public System.Action onRefresh;

	public RunningDirty(System.Action refreshAction, float refreshDelay, bool defaultDirty = false)
	{
		minimumRefreshDelay = refreshDelay;
		onRefresh += () => refreshAction?.Invoke();
		if (defaultDirty) MarkDirty();
	}

	public void MarkDirty() => MarkDirty(true);
	public void MarkDirty(bool dirty)
	{
		if (dirty) lastDirtyTicks = nowTicks;
		isDirty = dirty;
	}

	Coroutine activeCheckLoop;
	public void Hook(MonoBehaviour behaviour, float checkDelay = 0.02f)
	{
		if (activeCheckLoop != null) behaviour.StopCoroutine(activeCheckLoop);
		activeCheckLoop = behaviour.StartCoroutine(c_CheckDirtyLoop(checkDelay));
	}

	public void Unhook(MonoBehaviour behaviour)
	{
		if (activeCheckLoop == null) return;
		behaviour.StopCoroutine(activeCheckLoop);
	}

	IEnumerator c_CheckDirtyLoop(float checkDelay)
	{
		while (true)
		{
			yield return new WaitForSecondsRealtime(checkDelay);
			CheckDirty();
		}
	}

	public void CheckDirty()
	{
		if (!isDirty) return;
		if (dirtyForSeconds < minimumRefreshDelay) return;
		InvokeRefresh();
		isDirty = false;
	}

	void InvokeRefresh() => onRefresh?.Invoke();
}