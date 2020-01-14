using Dating.DataAccess.Managers;
using System.Web.Mvc;

namespace Dating.Controllers
{
	[Authorize]
	public class XmlController : Controller
	{
		private readonly ProfileManager _profileManager;
		
		public XmlController()
		{
			_profileManager = new ProfileManager();
		
		}

		//Export all profiles as xml file 
		public void ExportProfiles()
		{
			var profiles = _profileManager.GetAll();

			Response.ClearContent();
			Response.Buffer = true;
			Response.AddHeader("content-disposition", "attachment;filename = Profiles.xml");

			Response.ContentType = "text/xml";

			var serializer = new System.Xml.Serialization.XmlSerializer(profiles.GetType());
			serializer.Serialize(Response.OutputStream, profiles);
		}

	}
}