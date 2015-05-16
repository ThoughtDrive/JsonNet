namespace System.Web.Mvc
{
	using System.Text;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;
	
	/// <summary> 
	/// A simple Json Result that implements the Json.NET serializer with camel-casing applied
	/// to an object's properties and writes the object to the current HTTP response in JSON format.
	/// </summary> 
	public class JsonNetResult : JsonResult
	{
		/// <summary>
		/// Holds a reference to the object to serialize to JSON format.
		/// </summary> 
		private object _objectToSerialize;

		/// <summary>
		/// Initializes the JsonNetResult object.
		/// </summary>
		/// <param name="objectToSerialize">The object that needs to be serialized and passed to the UI in JSON format.</param>
		public JsonNetResult(object objectToSerialize)
		{
			_objectToSerialize = objectToSerialize;
			JsonRequestBehavior = JsonRequestBehavior.DenyGet;
		}

		/// <summary>
		/// Initializes the JsonNetResult object.
		/// </summary>
		/// <param name="objectToSerialize">The object that needs to be serialized and passed to the UI in JSON format.</param>
		/// <param name="jsonRequestBehavior">Specifies whether HTTP GET requests are allowed from the client.</param>
		public JsonNetResult(object objectToSerialize, JsonRequestBehavior jsonRequestBehavior)
		{
			_objectToSerialize = objectToSerialize;
			JsonRequestBehavior = jsonRequestBehavior;
		}

		/// <summary> 
		/// Serializes the object, applies camel-casing to its properties and writes it to the current HTTP response output stream in JSON format.
		/// </summary> 
		/// <param name="controllerContext">The current HTTP request context. Used to get the current HTTP response.</param> 
		public override void ExecuteResult(ControllerContext controllerContext)
		{
			// Throw an exception if the controller context is null (this means the HTTP request is null).
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}

			// Prevent GET requests by default unless the JSON request behavior has been specified to allow GET requests.
			if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(controllerContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
			}

			// Get the current HTTP response.
			var response = controllerContext.HttpContext.Response;

			// Set the serializer settings to use camel-casing.
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};

			// Set the default content type and content encoding for the response. These are specified by RFC 4627.
			// See http://www.ietf.org/rfc/rfc4627.txt.
			response.ContentEncoding = Encoding.UTF8;
			response.ContentType = "application/json";

			// If the object to serialize is not null, serialize it with camel-casing applied to its properties and write it to the current HTTP response stream in JSON format.
			if (_objectToSerialize != null)
			{
				response.Write(JsonConvert.SerializeObject(_objectToSerialize, Formatting.None, settings));
			}
		}
	}
}