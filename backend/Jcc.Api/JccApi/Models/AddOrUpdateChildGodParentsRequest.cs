using System.Collections.Generic;

namespace JccApi.Models
{
    public class AddOrUpdateChildGodParentsRequest
    {
        public string UserLogin { get; set; }
        public IEnumerable<AddOrUpdateGodParentModel> GodParents { get; set; } = new List<AddOrUpdateGodParentModel>();
    }
}
