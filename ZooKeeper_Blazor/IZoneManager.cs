using System;
using ZooKeeper_Blazor;

//Definding the ZoneManager interface
public interface IZoneManager
{
    void AddZoneWhenFull();
    void AddZones(Direction d);
    void CreateRandomDirection();
    bool IsWin();
}
