
using System.Collections.Generic;
using System.Linq;
using BusBoard.Api.Tfl;
using BusBoard.Api.Tfl.Responses;

namespace BusBoard.Web.ViewModels
{
  public class BusStopInfo
  {
    private TflApi tflApi = new TflApi();

    public IEnumerable<Prediction> Predictions { get; }
    public string Name;

    public BusStopInfo(StopPoint stopPoint)
    {
      Name = stopPoint.commonName;
      Predictions = tflApi.GetSortedArrivals(stopPoint.id).Take(5);
    }

    public static string FormatMinutes(int minutes) => minutes == 1 ? "1 min" : $"{minutes} mins";
  }
}