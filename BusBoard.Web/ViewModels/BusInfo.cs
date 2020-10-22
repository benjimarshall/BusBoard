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
    public BusStopInfo BusStop1 { get; }
    public BusStopInfo BusStop2 { get; }

    public string ErrorMessage { get; } = "";

    private TflApi tflApi = new TflApi();
    private PostcodeApi postcodeApi = new PostcodeApi();

    public BusInfo(string postCode)
    {
      try
      {
        var postcodeData = postcodeApi.GetPostcodeData(postCode);
        var stopPoints = tflApi.GetStopPointsAtLocation(postcodeData.latitude, postcodeData.longitude);

        var busStops = stopPoints.Select(stopPoint => new BusStopInfo(stopPoint));

        if (!busStops.Any())
        {
          ErrorMessage = "No bus stops near this location.";
          return;
        }

        BusStop1 = busStops.ElementAtOrDefault(0);
        BusStop2 = busStops.ElementAtOrDefault(1);

        ErrorMessage = "";
      }
      catch (Exception ex) when (ex is TflApiException || ex is PostcodeApiException)
      {
        ErrorMessage = (ex.Message);
      }
    }
  }
}