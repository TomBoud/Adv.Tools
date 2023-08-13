namespace Adv.Tools.Abstractions.Database
{
    public interface IReportProjectBasePoint
    {
        string Disicpline { get; set; }
        string ExpectedAngle { get; set; }
        string ExpectedEastWest { get; set; }
        string ExpectedElevation { get; set; }
        string ExpectedLatitude { get; set; }
        string ExpectedLongitude { get; set; }
        string ExpectedNorthSouth { get; set; }
        int Id { get; set; }
        bool IsBasePoint { get; set; }
        string IsBasePointHeb { get; set; }
        bool IsCorrect { get; set; }
        string IsCorrectHeb { get; set; }
        bool IsLocation { get; set; }
        string IsLocationHeb { get; set; }
        string LinkedAngle { get; set; }
        string LinkedEastWest { get; set; }
        string LinkedElevation { get; set; }
        string LinkedLatitude { get; set; }
        string LinkedLongitude { get; set; }
        string LinkedNorthSouth { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
    }
}