using System.Threading.Tasks;
using System.Web.Mvc;

namespace unboxed.web.Models
{
    public class PanelModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Task<ActionResult> ButtonTarget { get; set; }
        public string ButtonText { get; set; } = "Starten";
    }
}