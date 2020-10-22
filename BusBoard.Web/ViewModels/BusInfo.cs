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
    public BusStopInfo closestBusStop { get; }
    public BusStopInfo secondClosestBusStop { get; }

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
        }

        closestBusStop = busStops.ElementAtOrDefault(0);
        secondClosestBusStop = busStops.ElementAtOrDefault(1);
      }
      catch (Exception ex) when (ex is TflApiException || ex is PostcodeApiException)
      {
        ErrorMessage = (ex.Message);
      }
    }
  }
}