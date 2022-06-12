using Data.ValueObject.Tank;

namespace Entity.Player.Tank.TankBase
{
    public interface ITank
    {
        TankVO VO { get; }
        void Initialize(TankVO vo);
    }
}