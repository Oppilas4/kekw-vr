public interface IFryableObject
{
    bool IsFrying { get; set; }
    void StartFrying();
    void StopFrying();
}