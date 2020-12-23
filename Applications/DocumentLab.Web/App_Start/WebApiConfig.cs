﻿namespace DocumentLab.Web
{
  using System.Web.Http;
  using System.Web.Http.Cors;
 
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.MapHttpAttributeRoutes();
      var corsAttribute = new EnableCorsAttribute("*", "*", "*");
      config.EnableCors(corsAttribute);
    }
  }
}
