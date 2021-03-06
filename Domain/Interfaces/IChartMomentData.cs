using OptimalMotion2.Enums;

namespace OptimalMotion2.Domain
{
    public interface IChartMomentData
    {
        IAircraftId AircraftId { get; }
        IMoment Moment { get; }
        AircraftType Type { get; }
        ChartMomentDataType SubType { get; }
    }
}
