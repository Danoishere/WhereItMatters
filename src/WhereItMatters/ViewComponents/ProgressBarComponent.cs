using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereItMatters.ViewComponents
{
    [ViewComponent(Name = "ProgressBar")]
    public class ProgressBarComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(decimal maxValue, decimal currentValue, decimal additionalValue = 0)
        {
            ViewData["CurrentPercentage"] = (currentValue/maxValue) * 100;
            if(additionalValue > maxValue - currentValue)
            {
                additionalValue = maxValue - currentValue;
            }

            ViewData["AdditionalPercentage"] = (additionalValue / maxValue) * 100;
            return View();
        }
    }
}
