using Acadimia.Data.Models;

namespace Acadimia.Infrastructure.Dtos.Constants
{
    public class CreateEditConstant
    {
        public Constant Constant { get; set; }
        public List<Constant> Parents { get; set; }
    }
}
