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
    public IEnumerable<BusStopInfo> BusStops { get; } = new List<BusStopInfo>();
    public string ErrorMessage { get; } = "";

    private TflApi tflApi = new TflApi();
    private PostcodeApi postcodeApi = new PostcodeApi();

    public BusInfo(string postCode)
    {
      try
      {
        var postcodeData = postcodeApi.GetPostcodeData(postCode);
        var stopPoints = tflApi.GetStopPointsAtLocation(postcodeData.latitude, postcodeData.longitude);

        BusStops = stopPoints.Select(stopPoint => new BusStopInfo(stopPoint));

        ErrorMessage = "";
      }
      catch (Exception ex) when (ex is TflApiException || ex is PostcodeApiException)
      {
        ErrorMessage = (ex.Message);
      }
    }
  }
}