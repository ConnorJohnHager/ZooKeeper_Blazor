using System;
using ZooKeeper_Blazor;

public interface IZoneManager
{
    void AddZoneWhenFull();
    void AddZones(Direction d);
    void CreateRandomDirection();
    bool IsWin();
}
