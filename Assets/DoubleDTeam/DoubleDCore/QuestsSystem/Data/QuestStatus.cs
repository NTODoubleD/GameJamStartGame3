namespace DoubleDCore.QuestsSystem.Data
{
    public enum QuestStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Pending = 2, //приостановлен
        Completed = 3,
        Failed = 4,
        Abandoned = 5, //отменен
        Blocked = 6
    }
}