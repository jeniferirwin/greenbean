namespace Com.Technitaur.GreenBean.Core
{
    public interface IClimbable
    {
        bool CanJumpFrom { get; }
        bool CanJumpTo { get; }
        int UpSpeed { get; }
        int DownSpeed { get; }
    }
}
