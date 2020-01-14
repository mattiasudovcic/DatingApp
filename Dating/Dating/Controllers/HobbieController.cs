using Dating.DataAccess.Managers;
using Dating.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dating.Controllers
{
    [Authorize]
    public class HobbieController : Controller
    {
        private readonly HobbieManager _hobbieManager;

        public HobbieController() {
            _hobbieManager = new HobbieManager();
        }
        
        //Get all hobbies to be used in checkbox list. Create / Edit profile.
        [AllowAnonymous]
        public JsonResult GetJsonListCheckBoxHobbies(string selectedValue = null)
        {

            var resultOptions = _hobbieManager.GetAll();
            if (resultOptions != null)
            {
                var options = new List<JSONComboOptionEntity>();
                foreach (var hobbie in resultOptions)
                    options.Add(new JSONComboOptionEntity { Value = hobbie.Id.ToString(), Description = hobbie.Name });

                return GetJsonResultComboOptions(options, selectedValue);
            }
            else
            {
                return GetJsonResultError(new Exception("An error occurred while trying to retrieve all hobbies."));
            }
        }



        private JsonResult GetJsonResultComboOptions(IEnumerable<JSONComboOptionEntity> values, string selectedValue)
        {
            if (selectedValue == null)
                selectedValue = "";
            if (!string.IsNullOrEmpty(selectedValue))
                selectedValue = selectedValue.Trim().ToUpper();
            return Json(new
            {
                IsSucess = true,
                Options = values.Select(r => new
                {
                    DisplayText = r.GetDescription(),
                    Value = r.GetValue(),                  
                    Selected = (r.GetValue().Trim().Equals(selectedValue, StringComparison.InvariantCultureIgnoreCase))
                }).OrderBy(d => d.DisplayText)
            }, JsonRequestBehavior.AllowGet);
        }

        private JsonResult GetJsonResultError(Exception ex)
        {
            var jsonResult = Json(new
            {
                IsSucess = false,
                ErrorCode = "1",
                ex.Message

            });

            jsonResult.MaxJsonLength = int.MaxValue;
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }
    }
}