using Dating.Common.Models;
using Dating.DataAccess.Context;
using FluentValidation;
using System.Linq;

namespace Dating.Common.Validators
{
	//Validate that the user's name is unique.
	//Validates that the user has at least two hobbies.
	//It is used to validate the registration / editing of the profile.
	public class ProfileValidator : AbstractValidator<Profile>
	{
		public ProfileValidator()
		{
			RuleFor(x => x.UserName).Must(BeUniqueUrl).WithMessage("Username already exists");
			RuleFor(x => x.Hobbies).Must(list => list != null && list.Count >= 3).WithMessage("Please select more than two hobbies.");

		}


		private bool BeUniqueUrl(string userName)
		{
			return new DatingContext().Profiles.FirstOrDefault(x => x.UserName == userName) == null;
		}

	}
}