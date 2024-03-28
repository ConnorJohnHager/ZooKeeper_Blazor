using System;
using ZooKeeper_Blazor;

// from Sunny
public interface IZoneManager
{
    void AddZoneWhenFull();
    void AddZones(Direction d);
    string CreateRandomDirection();
    bool IsWin();
}
