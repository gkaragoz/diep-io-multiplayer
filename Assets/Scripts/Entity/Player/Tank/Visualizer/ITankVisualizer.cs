using Data.ValueObject.Tank;
using Enums;

namespace Entity.Player.Tank.Visualizer
{
    public interface ITankVisualizer
    {
        void Initialize(TankVO vo);
        void SetVisualization(TeamType team);
    }
}