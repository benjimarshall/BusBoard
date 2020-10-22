using System;
using System.Collections.Generic;
using System.Linq;
using BusBoard.Api.Postcode;
using BusBoard.Api.Tfl;
using BusBoard.Api.Tfl.Responses;

namespace BusBoard.Web.ViewModels
{
  public class BusInfo
  {
    public IEnumerable<StopPoint> StopPoints { get; } = new List<StopPoint>();
    public string ErrorMessage { get; } = "";

    private TflApi tflApi = new TflApi();
    private PostcodeApi postcodeApi = new PostcodeApi();

    public BusInfo(string postCode)
    {
      try
      {
        var postcodeData = postcodeApi.GetPostcodeData(postCode);

        StopPoints = tflApi.GetStopPointsAtLocation(postcodeData.latitude, postcodeData.longitude);
        ErrorMessage = "";
      }
      catch (Exception ex) when (ex is TflApiException || ex is PostcodeApiException)
      {
        ErrorMessage = (ex.Message);
      }
    }

    public string PostCode { get; set; }

    public IEnumerable<Prediction> GetPredictions(StopPoint stop)
    {
      return tflApi.GetSortedArrivals(stop.id);
    }

    public static string FormatMinutes(int minutes) => minutes == 1 ? "1 min" : $"{minutes} mins";
  }
}