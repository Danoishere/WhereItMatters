using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereItMatters.Core;
using Microsoft.AspNetCore.Html;

namespace WhereItMatters.DataAccess
{
    public class LocalizationRepository : BaseRepository<LocalizedText>
    {
        public LocalizationRepository(DbContext dbContext) : base(dbContext){}

        public async Task<HtmlString> GetHtml(string identifier)
        {
            var text = await Get(identifier);

            return new HtmlString(text);
        }

        public async Task<string> Get(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                return "";
            }

            var localizedText = await SearchFor(l => l.Identifier.ToLower() == identifier.ToLower()).FirstOrDefaultAsync();
            if(localizedText == null)
            {
                await Insert(new LocalizedText { Identifier = identifier, ValueEN = "(To be completed)" });
                localizedText = await SearchFor(l => l.Identifier.ToLower() == identifier.ToLower()).FirstOrDefaultAsync();
            }

            return localizedText.ValueEN ?? "";   
         }
    }
}
