using UnityEngine.Events;

[System.Serializable]
public class AvatarUnityEvent : UnityEvent<AvatarController> { }

public class AvatarTriggerZone : BaseTriggerZone<AvatarUnityEvent, AvatarController> {}