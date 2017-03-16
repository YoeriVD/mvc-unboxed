using System.Collections.Generic;

namespace unboxed.web.Models.Forms
{
    public class LookupViewModel
    {
        public string Value { get; set; }
        public IEnumerable<LookupItem> PossibleValues { get; set; }
    }
}