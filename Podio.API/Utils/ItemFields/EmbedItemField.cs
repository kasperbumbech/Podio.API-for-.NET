using Podio.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API.Utils.ItemFields
{
    public class EmbedItemField : ItemField
    {
        private List<Embed> _embeds;

        public IEnumerable<Embed> Embeds
        {
            get
            {
                if (_embeds == null)
                {
                    _embeds = new List<Embed>();
                    foreach (var embedFilePair in this.Values)
                    {
                        var embed = this.valueAs<Embed>(embedFilePair, "embed");
                        if (embedFilePair.ContainsKey("file"))
                        {
                            var file = this.valueAs<FileAttachment>(embedFilePair, "file");
                            if (embed.Files == null) {
                                embed.Files = new List<FileAttachment>();
                            }
                            embed.Files.Add(file);
                        }
                        _embeds.Add(embed);
                    }
                }
                return _embeds;
            }
        }
    }
}
