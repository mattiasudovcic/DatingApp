using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dating.Helpers
{
	public class JSONComboOptionEntity
	{
        public JSONComboOptionEntity()
        {
            this.Habilitado = true;
        }

        public string GetDescription()
        {
            return this.Description;
        }

        public string GetValue()
        {
            return this.Value;
        }
    
        public string Value { get; set; }

        public string Description { get; set; }

        public bool Habilitado { get; set; }

    }
}