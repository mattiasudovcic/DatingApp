using Dating.Common.Models;
using Dating.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dating.DataAccess.Managers
{
	public class HobbieManager
	{
		private readonly HobbieRepository _hobbieRepository;

		public HobbieManager()
		{
			_hobbieRepository = new HobbieRepository();
		}

		//Get all the hobbies
		public List<Hobbie> GetAll()
		{
			try
			{
				return _hobbieRepository.GetAll();

			}
			catch (Exception e)
			{
				//Log Error
				throw new Exception("An error occurred while trying to retrieve all hobbies.");
			}
		}

	}
}