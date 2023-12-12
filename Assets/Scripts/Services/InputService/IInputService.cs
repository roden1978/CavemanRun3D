namespace HalfDiggers.Runner
{
    public interface IInputService
    {
       float Horizontal { get;}
       float Vertical { get;}
       void Update();
    }
}