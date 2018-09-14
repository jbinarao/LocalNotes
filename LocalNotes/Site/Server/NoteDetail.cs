using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalNotes.Site.Server
{
    public class NoteDetail
    {
        // Properties for note detail.
        public int NoteID { get; set; }
        public string NoteName { get; set; }
        public string NoteText { get; set; }
        public DateTime NoteTime { get; set; }
        public string NoteAbbr { get; set; }
        public string Modified { get; set; }
    }
}