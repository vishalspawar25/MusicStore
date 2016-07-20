using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServerDemo1.UI.Models.Blocks;

namespace EPiServerDemo1.UI.Controllers
{
    public class ExperienceController : BlockController<Experience>
    {
        public override ActionResult Index(Experience currentBlock)
        {
            return PartialView(currentBlock);
        }
    }
}
