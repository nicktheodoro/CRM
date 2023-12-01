using FluentValidation.Results;
using MyApp.SharedDomain.ValueObjects;

namespace User.Core.Models.User.Image
{
    public class ImageModel : Entity
    {
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public virtual required UserModel UserMaster { get; set; }

        public override bool Valid(out ValidationResult validationResult)
        {
            throw new NotImplementedException();
        }
    }
}
