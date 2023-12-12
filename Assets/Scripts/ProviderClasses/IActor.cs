﻿namespace HalfDiggers.Runner
{
    public interface IActor
    {
        int Entity { get; }
        void Handle();
        void AddEntity(int entity);
    }
}