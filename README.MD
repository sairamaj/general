## AspNetWebApiRoutesHelper
Custom serialzier for ASP.NET Routes information. Helps in debugging Route issues.

## Usage
```c#
    // in standard ASPNET Web api Startup class
    // Make sure Http configuration is initialized so that routes information is populated.
    config.EnsureInitialized();
    // Call to get API routes
    var apiRoutes = config.GetApiRoutes();
    
    // Write to custom file to view the information.
    File.WriteAllText(@"c:\temp\apiroutes.json", JsonConvert.SerializeObject(apiRoutes, Formatting.Indented));
```

## Sample output
```json
[
  {
    "Name": "",
    "Defaults": {},
    "Constraints": {},
    "DataTokens": {},
    "IsSubRoute": false,
    "SubRoutes": [
      {
        "Name": "api/listitems",
        "Defaults": {},
        "Constraints": {},
        "DataTokens": {
          "actions": [
            {
              "TypeName": "System.Web.Http.Controllers.ReflectedHttpActionDescriptor",
              "Properties": {
                "actionName": "Get",
                "Microsoft.Web.Http.Versioning.ApiVersionModel": {
                  "IsApiVersionNeutral": false,
                  "DeclaredApiVersions": [
                    {
                      "GroupVersion": null,
                      "MajorVersion": 1,
                      "MinorVersion": 0,
                      "Status": null
                    }
                  ],
                  "ImplementedApiVersions": [
                    {
                      "GroupVersion": null,
                      "MajorVersion": 1,
                      "MinorVersion": 0,
                      "Status": null
                    },
                    {
                      "GroupVersion": null,
                      "MajorVersion": 2,
                      "MinorVersion": 0,
                      "Status": null
                    }
                  ],
                  "SupportedApiVersions": [
                    {
                      "GroupVersion": null,
                      "MajorVersion": 1,
                      "MinorVersion": 0,
                      "Status": null
                    },
                    {
                      "GroupVersion": null,
                      "MajorVersion": 2,
                      "MinorVersion": 0,
                      "Status": null
                    }
                  ],
                  "DeprecatedApiVersions": []
                },
                "MS_IsAttributeRouted": true
              }
            }
          ],
          "precedence": [
            {
              "TypeName": "System.Decimal",
              "Properties": {
                "precedence": 1.1
              }
            }
          ]
        },
        "IsSubRoute": true,
        "SubRoutes": null
      },
      {
        "Name": "api/listitems",
        "Defaults": {},
        "Constraints": {},
        "DataTokens": {
          "actions": [
            {
              "TypeName": "System.Web.Http.Controllers.ReflectedHttpActionDescriptor",
              "Properties": {
                "actionName": "Get",
                "Microsoft.Web.Http.Versioning.ApiVersionModel": {
                  "IsApiVersionNeutral": false,
                  "DeclaredApiVersions": [],
                  "ImplementedApiVersions": [
                    {
                      "GroupVersion": null,
                      "MajorVersion": 1,
                      "MinorVersion": 0,
                      "Status": null
                    },
                    {
                      "GroupVersion": null,
                      "MajorVersion": 2,
                      "MinorVersion": 0,
                      "Status": null
                    }
                  ],
                  "SupportedApiVersions": [
                    {
                      "GroupVersion": null,
                      "MajorVersion": 1,
                      "MinorVersion": 0,
                      "Status": null
                    },
                    {
                      "GroupVersion": null,
                      "MajorVersion": 2,
                      "MinorVersion": 0,
                      "Status": null
                    }
                  ],
                  "DeprecatedApiVersions": []
                },
                "MS_IsAttributeRouted": true
              }
            }
          ],
          "precedence": [
            {
              "TypeName": "System.Decimal",
              "Properties": {
                "precedence": 1.1
              }
            }
          ]
        },
        "IsSubRoute": true,
        "SubRoutes": null
      }
    ]
  },
  {
    "Name": "api/{controller}/{id}",
    "Defaults": {
      "id": ""
    },
    "Constraints": {},
    "DataTokens": {},
    "IsSubRoute": false,
    "SubRoutes": []
  }
]
```