using UnityEngine.Events;
using Pulse.Experimental;

[System.Serializable]
public class AvatarUnityEvent : UnityEvent<AvatarController> { }

public class AvatarTriggerZone : BaseTriggerZone<AvatarUnityEvent, AvatarController> {}