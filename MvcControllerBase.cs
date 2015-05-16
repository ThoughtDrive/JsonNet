namespace System.Web.Mvc
{
	public class MvcControllerBase : Controller
	{
		/// <summary>
		/// Initializes the JsonNetController object.
		/// </summary>
		/// <param name="objectToSerialize">The object that needs to be serialized and passed to the UI in JSON format.</param>
		/// <returns>Returns a new JsonNetResult which contains the serialized object in JSON format with camel-casing applied to all of the object's properties.</returns>
		public JsonNetResult JsonNet(object objectToSerialize)
		{
			return new JsonNetResult(objectToSerialize);
		}

		/// <summary>
		/// Initializes the JsonNetController object.
		/// </summary>
		/// <param name="objectToSerialize">The object that needs to be serialized and passed to the UI in JSON format.</param>
		/// <param name="jsonRequestBehavior">Specifies whether HTTP GET requests are allowed from the client.</param>
		/// <returns>Returns a new JsonNetResult which contains the serialized object in JSON format with camel-casing applied to all of the object's properties.</returns>
		public JsonNetResult JsonNet(object objectToSerialize, JsonRequestBehavior jsonRequestBehavior)
		{
			return new JsonNetResult(objectToSerialize, jsonRequestBehavior);
		}
	}
}