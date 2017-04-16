using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereItMatters.Core;

namespace WhereItMatters.ViewComponents
{
    [ViewComponent(Name = "DonationBox")]
    public class DonationBoxComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(DonationType donationType, int targetId)
        {
            string formAction = "";
            string targetIdField = "";

            if(donationType == DonationType.DonationRequest)
            {
                formAction = "DonationDetailsForRequest";
                targetIdField = "requestId";
            }
            else if (donationType == DonationType.Mission)
            {
                formAction = "DonationDetailsForMission";
                targetIdField = "missionId";
            }
            else if (donationType == DonationType.Organisation)
            {
                formAction = "DonationDetailsForOrganisation";
                targetIdField = "organisationId";
            }

            ViewData["FormAction"] = formAction;
            ViewData["TargetIdField"] = targetIdField;
            ViewData["TargetId"] = targetId;

            return View();
        }
    }
}
